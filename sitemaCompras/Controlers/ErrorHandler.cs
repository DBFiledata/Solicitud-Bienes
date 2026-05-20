namespace sitemaCompras.Controlers
{
    using System;
    using System.Web.UI;

    public static class ErrorHandler
    {
        /// <summary>
        /// Log an exception via System.Diagnostics.Trace.
        /// </summary>
        public static void LogError(Exception ex, string methodName, string context = "")
        {
            var message = string.Format(
                "[{0:yyyy-MM-dd HH:mm:ss}] {1} — {2}: {3}\n{4}",
                DateTime.Now,
                methodName,
                ex.GetType().Name,
                ex.Message,
                ex.StackTrace);

            if (!string.IsNullOrEmpty(context))
            {
                message = string.Format(
                    "[{0:yyyy-MM-dd HH:mm:ss}] {1} [ctx: {2}] — {3}: {4}\n{5}",
                    DateTime.Now,
                    methodName,
                    context,
                    ex.GetType().Name,
                    ex.Message,
                    ex.StackTrace);
            }

            System.Diagnostics.Trace.TraceError(message);
        }

        /// <summary>
        /// Log a string message (non-exception) via System.Diagnostics.Trace.
        /// </summary>
        public static void LogError(string message, string methodName, string context = "")
        {
            var entry = string.Format(
                "[{0:yyyy-MM-dd HH:mm:ss}] {1} — {2}",
                DateTime.Now,
                methodName,
                message);

            if (!string.IsNullOrEmpty(context))
            {
                entry = string.Format(
                    "[{0:yyyy-MM-dd HH:mm:ss}] {1} [ctx: {2}] — {3}",
                    DateTime.Now,
                    methodName,
                    context,
                    message);
            }

            System.Diagnostics.Trace.TraceError(entry);
        }

        /// <summary>
        /// Show a user-facing error alert via JavaScript.
        /// Uses ScriptManager.RegisterStartupScript when available; falls back to ClientScript.
        /// </summary>
        public static void ShowUserError(Page page, string action, string detail = "")
        {
            if (page == null)
                return;

            var message = "Error al " + action + ".";
            if (!string.IsNullOrEmpty(detail))
                message += " " + detail;

            // Escape single quotes for JavaScript
            var safeMessage = message.Replace("'", "\\'");

            var script = "alert('" + safeMessage + "');";
            var key = "Error_" + Guid.NewGuid().ToString("N");

            var sm = ScriptManager.GetCurrent(page);
            if (sm != null && sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), key, script, addScriptTags: true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), key, script, addScriptTags: true);
            }
        }
    }
}
