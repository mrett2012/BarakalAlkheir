using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarakalAlkheir.components
{
    // Keeps the existing synchronous write/transaction handlers unchanged.
    // Background readers must create/dispose their own context and return materialized data.
    internal static class FormLoading
    {
        private static readonly ConditionalWeakTable<Form, Dictionary<string, object>> lookupCache =
            new ConditionalWeakTable<Form, Dictionary<string, object>>();
        private sealed class Lifetime { public bool Closed; }
        private static readonly ConditionalWeakTable<Form, Lifetime> lifetimes = new ConditionalWeakTable<Form, Lifetime>();

        public static void Attach(Form form, EventHandler load)
        {
            Attach(form, (sender, args) => { load(sender, args); return Task.CompletedTask; });
        }

        public static void Attach(Form form, Func<object, EventArgs, Task> load)
        {
            bool started = false;
            form.Shown += async (sender, args) =>
            {
                if (started || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
                started = true;
                await RunAsync(form, () => load(sender, args));
            };
        }

        public static async Task RunAsync(Form form, Func<Task> load)
        {
            if (form.IsDisposed || form.Disposing) return;
            using (var busy = new BusyScope(form))
            {
                try
                {
                    // Return to the normal message loop so the form and indicator paint first.
                    await Task.Delay(50);
                    ThrowIfClosed(form);
                    await load();
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    if (!form.IsDisposed && !form.Disposing)
                        MessageBox.Show(form, ex.Message, form.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static async Task LoadAsync<T>(Form form, Func<T> read, Action<T> bind, bool background = true, string cacheKey = null)
        {
            if (!background)
            {
                // Used by existing synchronous refreshes inside write transactions.
                bind(read());
                return;
            }
            ThrowIfClosed(form);
            Dictionary<string, object> cache;
            object cached;
            if (cacheKey != null && lookupCache.TryGetValue(form, out cache) && cache.TryGetValue(cacheKey, out cached))
            {
                bind((T)cached);
                return;
            }
            T data;
            try { data = await Task.Run(read); }
            catch { ThrowIfClosed(form); throw; }
            ThrowIfClosed(form);
            if (cacheKey != null && lookupCache.TryGetValue(form, out cache)) cache[cacheKey] = data;
            bind(data);
        }

        public static async Task ReadAsync(Form form, Action read)
        {
            ThrowIfClosed(form);
            try { await Task.Run(read); }
            catch { ThrowIfClosed(form); throw; }
            ThrowIfClosed(form);
        }

        public static async Task<T> ReadAsync<T>(Form form, Func<T> read)
        {
            ThrowIfClosed(form);
            T result;
            try { result = await Task.Run(read); }
            catch { ThrowIfClosed(form); throw; }
            ThrowIfClosed(form);
            return result;
        }

        public static void ThrowIfClosed(Form form)
        {
            Lifetime lifetime;
            if (form == null || form.IsDisposed || form.Disposing ||
                (lifetimes.TryGetValue(form, out lifetime) && lifetime.Closed)) throw new OperationCanceledException();
        }

        private sealed class BusyScope : IDisposable, IMessageFilter
        {
            private readonly Form form;
            private readonly Panel panel;
            private readonly bool previousWaitCursor;

            public BusyScope(Form form)
            {
                this.form = form;
                lookupCache.GetOrCreateValue(form);
                previousWaitCursor = form.UseWaitCursor;
                panel = new Panel { Name = "LoadingStatus", Dock = DockStyle.Bottom, Height = 48,
                    BackColor = Color.FromArgb(240, 246, 255), Padding = new Padding(12, 6, 12, 6) };
                var label = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight,
                    RightToLeft = RightToLeft.Yes, Text = "جاري تحميل البيانات... / Loading data..." };
                var progress = new ProgressBar { Dock = DockStyle.Left, Width = 150,
                    Style = ProgressBarStyle.Marquee, MarqueeAnimationSpeed = 25 };
                panel.Controls.Add(label);
                panel.Controls.Add(progress);
                form.Controls.Add(panel);
                panel.BringToFront();
                form.UseWaitCursor = true;
                Application.AddMessageFilter(this);
                form.FormClosed += OnClosed;
            }

            private void OnClosed(object sender, FormClosedEventArgs args)
            {
                // Closing a modal form hides it without necessarily disposing it.
                lifetimes.GetOrCreateValue(form).Closed = true;
                Application.RemoveMessageFilter(this);
                lookupCache.Remove(form);
            }

            public bool PreFilterMessage(ref Message message)
            {
                // Block input into partially populated controls without changing permission states.
                bool input = (message.Msg >= 0x100 && message.Msg <= 0x109) ||
                    (message.Msg >= 0x201 && message.Msg <= 0x20E);
                if (!input || form.IsDisposed) return false;
                Control target = Control.FromChildHandle(message.HWnd);
                return target != null && target.FindForm() == form;
            }

            public void Dispose()
            {
                Application.RemoveMessageFilter(this);
                lookupCache.Remove(form);
                form.FormClosed -= OnClosed;
                if (!form.IsDisposed)
                {
                    form.Controls.Remove(panel);
                    form.UseWaitCursor = previousWaitCursor;
                }
                panel.Dispose();
            }
        }
    }
}
