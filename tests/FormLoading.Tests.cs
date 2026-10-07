using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarakalAlkheir.components;

internal static class FormLoadingTests
{
    private static int checks;
    private static int exitCode;
    private static void Check(bool condition, string message)
    {
        checks++;
        if (!condition) throw new Exception(message);
    }

    private static Form NewForm()
    {
        return new Form { ShowInTaskbar = false, StartPosition = FormStartPosition.Manual,
            Location = new Point(-20000, -20000), Size = new Size(500, 300) };
    }

    private static async Task RunChecks()
    {
        int uiThread = Thread.CurrentThread.ManagedThreadId;
        int ticks = 0;
        using (var timer = new System.Windows.Forms.Timer { Interval = 10 })
        using (var form = NewForm())
        {
            timer.Tick += (sender, args) => ticks++;
            timer.Start();
            form.Show();
            bool bound = false;
            int reads = 0;
            var deniedButton = new Button { Enabled = false, Visible = false };
            form.Controls.Add(deniedButton);
            await FormLoading.RunAsync(form, async () =>
            {
                Check(form.Visible, "Form must be visible before data loading starts.");
                Check(form.Controls.ContainsKey("LoadingStatus"), "Loading indicator must appear before the read.");
                int before = ticks;
                await FormLoading.LoadAsync(form, () =>
                {
                    Check(Thread.CurrentThread.ManagedThreadId != uiThread, "Database work must run off the UI thread.");
                    Thread.Sleep(200);
                    return ++reads;
                }, result =>
                {
                    Check(Thread.CurrentThread.ManagedThreadId == uiThread, "Control binding must run on the UI thread.");
                    Check(result == 1, "Reader result must reach the binder.");
                    bound = true;
                }, true, "lookup");
                Check(ticks > before + 2, "UI timer must keep ticking while the reader is busy.");
                await FormLoading.LoadAsync(form, () => ++reads, result => Check(result == 1, "Reuse lookup within one load."), true, "lookup");
            });
            Check(bound && reads == 1, "Repeated lookups should execute once per load.");
            Check(!form.Controls.ContainsKey("LoadingStatus") && !form.UseWaitCursor, "Busy state must clear.");
            Check(!deniedButton.Enabled && !deniedButton.Visible, "Loading must not re-enable permission-controlled controls.");

            await FormLoading.RunAsync(form, () => FormLoading.LoadAsync(form, () => ++reads,
                result => Check(result == 2, "A later load must query fresh data."), true, "lookup"));
            Check(reads == 2, "Lookup cache must not survive load completion.");

            bool failureObserved = false;
            try { await FormLoading.LoadAsync<int>(form, () => { throw new InvalidOperationException("Expected test failure"); }, value => { }); }
            catch (InvalidOperationException) { failureObserved = true; }
            Check(failureObserved, "Read failures must propagate for display by the caller.");
            form.Close();
        }

        using (var form = NewForm())
        using (var gate = new ManualResetEventSlim())
        {
            bool bound = false;
            var started = new TaskCompletionSource<bool>();
            form.Show();
            Task pending = FormLoading.RunAsync(form, () => FormLoading.LoadAsync(form, () =>
            {
                started.SetResult(true);
                if (!gate.Wait(5000)) throw new TimeoutException("Test gate was not released.");
                return 1;
            }, value => bound = true));
            await started.Task;
            form.Close();
            gate.Set();
            await pending;
            Check(!bound, "Closing a loading form must discard the result.");
        }

        using (var form = NewForm())
        {
            int loaded = 0;
            var completed = new TaskCompletionSource<bool>();
            FormLoading.Attach(form, new Func<object, EventArgs, Task>(async (sender, args) =>
            {
                Check(form.Visible, "Attached initialization must wait for Shown.");
                await Task.Delay(20);
                loaded++;
                completed.SetResult(true);
            }));
            form.Show();
            Check(loaded == 0, "Showing the window must not synchronously load data.");
            await completed.Task;
            await Task.Delay(20);
            form.Hide();
            form.Show();
            await Task.Delay(100);
            Check(loaded == 1, "Repeated showing must not duplicate initial loading.");
            form.Close();
        }

        using (var modal = NewForm())
        using (var gate = new ManualResetEventSlim())
        using (var closer = new System.Windows.Forms.Timer { Interval = 10 })
        {
            bool bound = false;
            var started = new TaskCompletionSource<bool>();
            Task pending = null;
            modal.Shown += (sender, args) => pending = FormLoading.RunAsync(modal,
                () => FormLoading.LoadAsync<int>(modal, () =>
                {
                    started.SetResult(true);
                    if (!gate.Wait(5000)) throw new TimeoutException("Modal test gate was not released.");
                    throw new InvalidOperationException("Expected read failure after modal close");
                }, value => bound = true));
            closer.Tick += (sender, args) =>
            {
                if (!started.Task.IsCompleted) return;
                closer.Stop();
                modal.DialogResult = DialogResult.Cancel;
                modal.Close();
                gate.Set();
            };
            closer.Start();
            modal.ShowDialog();
            Check(!modal.IsDisposed, "Test must cover a modal form closed without disposal.");
            await pending;
            Check(!bound, "Closing a modal form must also discard the result.");
        }
        Console.WriteLine("PASS: " + checks + " loading lifecycle/threading/cache checks; no database access.");
    }

    [STAThread]
    private static int Main()
    {
        Control.CheckForIllegalCrossThreadCalls = true;
        Application.EnableVisualStyles();
        using (var host = NewForm())
        {
            host.Shown += async (sender, args) =>
            {
                try { await RunChecks(); }
                catch (Exception exception) { Console.Error.WriteLine(exception); exitCode = 1; }
                finally { host.Close(); }
            };
            Application.Run(host);
        }
        return exitCode;
    }
}
