# Application date format

The single full-date display setting is `AppDateFormat.Pattern` in `BarakatAlkhreir/AppDateFormat.cs`:

```csharp
public const string Pattern = "dd/MM/yyyy";
```

Rebuild the application after changing this source setting. `Program.Main` calls `AppDateFormat.ApplyToCurrentThread()` before constructing any forms. The application uses Gregorian dates with fixed slashes, independently of Windows short-date settings. The culture setup preserves number formatting, time formatting and `CurrentUICulture` (interface language).

All existing full-date picker and Telerik grid formats reference the shared setting. `ToShortDateString()` and existing culture-based date parsing use the initialized date culture; explicit full-date report formatting uses the shared helper. Telerik date pickers use its read-only date culture. Month/year and year selectors keep their intended scope instead of showing a complete date. DateTime columns remain typed dates in SQL/EF; their display pattern is not a database format.

New code should read date pickers through `Value`, assign typed date values, and use `AppDateFormat.Format(date)` for date text. Use `AppDateFormat.PickerFormat` for native/Telerik date controls and `AppDateFormat.GridFormat` with `AppDateFormat.Culture` for grids. This .NET Framework 4.0 application has no application-owned background workers; a future worker entry point must also call `ApplyToCurrentThread()` if it uses culture-based formatting/parsing.

The employee application date is a legacy text column. Its load and report paths accept the existing year-first strings and the shared display format; saving uses the shared format. Unknown historical text is preserved for report output. No historical records were rewritten.

Changing the setting to a different date order in the future also requires reviewing masked date inputs, date strings stored in records and legacy code that constructs day/month/year strings. This change establishes `dd/MM/yyyy` as the consistent application format; it does not add an end-user format selector or a schema migration.

Run the isolated build and culture/control checks without database access:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tests\Run-GlobalDateFormatTests.ps1
```

Validated on 2026-09-16: the isolated x86 build passed with existing warnings. The global-date harness passed 2,316 checks under en-US, en-GB, ar-KW, ar-SA and th-TH, including 630 picker date cases, 210 grid-format cases and 120 masked-date cases. The existing 233 licence rule/date-control checks also passed. No database operations were performed.

These checks validate source compilation and managed date/control behavior. Crystal Reports rendering/printing, Excel COM auto-formatting and production database workflows remain separate runtime acceptance checks.
