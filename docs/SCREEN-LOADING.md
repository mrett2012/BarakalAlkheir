# Screen loading and document-list performance

Implemented on 2026-10-07. Live database timings have not been measured.

## Opening screens

The existing designer `Load` event wiring is preserved. The source Load handlers for 69 data forms register their initialization through `components/FormLoading.cs`. Initialization starts after `Shown` and an opportunity to paint. The main application's authentication gate keeps its existing lifecycle.

The form displays `جاري تحميل البيانات... / Loading data...`, a marquee progress bar, and a wait cursor. Input into the loading form is blocked without changing individual controls' Enabled/Visible states, so permission decisions are preserved. The title-bar close button remains usable. Late results and read failures after closing normal or modal windows are discarded rather than applied to closed controls.

Database work explicitly separated from UI binding runs on worker threads. Each reader creates and disposes its own EF context; controls are updated on the UI thread. Existing synchronous refresh paths used by write handlers remain available and do not introduce awaits into transaction scopes.

The shared CRUD toolbar also reads permissions in the background during initialization. Restricted buttons stay hidden until the read completes, and each caller awaits permission application before applying its screen-specific visibility settings. Existing event names, permission joins, and superuser behavior are preserved.

The background paths cover the six document lists below, the main entity lists in definition/employee/sponsor/rental forms, and all 25 shared dropdown loaders. Some legacy report generation and secondary detail-binding queries remain synchronous; displaying the window first does not make those operations non-blocking. Database/Crystal/Excel workflows still require an authenticated manual check.

## Query and rendering changes

The employee, licence, and car expiry lists, daily document tasks, unpaid tasks, and task-step lists now:

- Fetch transaction names and applicable licence remarks/unpaid-rental flags in batches instead of querying those values separately for each displayed row.
- Restrict each ID batch to 500 IDs, below SQL Server's parameter limit. Empty and unselected ID sets do not generate unnecessary lookups.
- Apply the existing expiry cutoff in SQL before materialization. Missing warning days use zero advance-warning days.
- Preserve employee/licence/car ownership joins, active/approved/paid/open filters, and each screen's distinction between open task names and all task names.
- Build grid rows inside BeginUpdate/EndUpdate, styling the actual added row. Telerik's CurrentRow can be null during batch population.
- Clear the previous display before a full refresh, and keep absent expiry dates, remaining-day values, and amounts blank.

Task-step selection also loads its detail rows in the background with the same busy indication.

Shared dropdown queries project their required ID/display fields instead of complete entities (the car-brand list remains a scalar query). Repeated dropdown requests reuse materialized results only during the same form-loading operation, keyed by lookup and arguments. Completion/closure drops that cache; later loads and synchronous write-handler refreshes query fresh data. Dropdown item population is also batched.

No database schema, indexes, stored values, financial calculations, or payment/task transaction boundaries were changed.

## Validation

Build with the Visual Studio MSBuild command in AGENTS.md, using `.codex-build\responsive-loading` for isolated output. Run:

```powershell
& .\tests\Run-FormLoadingTests.ps1
& .\tests\Run-DocumentListLoadingTests.ps1
& .\tests\Run-TelerikUpgradeTests.ps1 -BuildBin .\.codex-build\responsive-loading\bin
```

The loading harness checks UI-thread binding, a responsive message loop while a reader is blocked, initial-load ordering, error propagation, normal/modal closure, permission-state preservation, lookup reuse, cache expiry, and duplicate initial-load prevention. The document harness checks ID batches larger than 2,100 and batch population/styling on all six real document-grid types without showing or loading their database-backed forms.

Verified: isolated .NET Framework 4.8/x86 build with Telerik 2026.3.812.48; 19 loading checks; document batching, six grids and six toolbar permission mappings; 2,316 date-format checks; 233 non-database rental checks; Telerik construction and Arabic-grid rendering smoke checks. Existing compiler warnings remain. No live database or authenticated application workflows were exercised.

Also run the existing date-format and non-database rental harnesses against the new build. These tests do not establish production response times, SQL query plans, authenticated permissions, or report/export behavior. Compare opening/refreshing the same document screens on a designated test database before making numerical performance claims.
