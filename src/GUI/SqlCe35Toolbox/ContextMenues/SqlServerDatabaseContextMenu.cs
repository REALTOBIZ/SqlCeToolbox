using System.Windows.Controls;
using System.Windows.Input;
using ErikEJ.SqlCeToolbox.Commands;
using ErikEJ.SqlCeToolbox.Helpers;
using ErikEJ.SqlCeToolbox.ToolWindows;

namespace ErikEJ.SqlCeToolbox.ContextMenues
{
    public class SqlServerDatabaseContextMenu : ContextMenu
    {
        public SqlServerDatabaseContextMenu(DatabaseMenuCommandParameters databaseMenuCommandParameters, ExplorerToolWindow parent)
        {
            var dcmd = new DatabasesMenuCommandsHandler(parent);
            bool isSqlCe40Installed = DataConnectionHelper.IsV40Installed();
            
            var scriptGraphCommandBinding = new CommandBinding(DatabaseMenuCommands.DatabaseCommand,
                            dcmd.GenerateServerDgmlFiles);
            var scriptDatabaseGraphMenuItem = new MenuItem
            {
                Header = "Create SQL Server Database Graph (DGML)...",
                Icon = ImageHelper.GetImageFromResource("../resources/Diagram_16XLG.png"),
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters,
            };
            scriptDatabaseGraphMenuItem.CommandBindings.Add(scriptGraphCommandBinding);
            Items.Add(scriptDatabaseGraphMenuItem);

            Items.Add(new Separator());

            var scriptDatabaseRootMenuItem = new MenuItem
            {
                Header = "Script SQL Server Database",
                Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
            };

            var toolTip = new ToolTip();
            toolTip.Content = "Generate a SQL Server Compact compatible database script from SQL Server 2005+";

            var scriptDatabaseCommandBinding = new CommandBinding(DatabaseMenuCommands.DatabaseCommand,
                                                                dcmd.ScriptServerDatabase);

            var scriptDatabaseSchemaMenuItem = new MenuItem
                                             {
                                                 Header = "Script SQL Server Database Schema...",
                                                 Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
                                                 ToolTip = toolTip,
                                                 Command = DatabaseMenuCommands.DatabaseCommand,
                                                 CommandParameter = databaseMenuCommandParameters,
                                                 Tag = SqlCeScripting.Scope.Schema
                                             };
            scriptDatabaseSchemaMenuItem.CommandBindings.Add(scriptDatabaseCommandBinding);
            scriptDatabaseRootMenuItem.Items.Add(scriptDatabaseSchemaMenuItem);

            var scriptDatabaseDataMenuItem = new MenuItem
            {
                Header = "Script SQL Server Database Data...",
                Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
                ToolTip = toolTip,
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters,
                Tag = SqlCeScripting.Scope.DataOnly
            };
            scriptDatabaseDataMenuItem.CommandBindings.Add(scriptDatabaseCommandBinding);
            scriptDatabaseRootMenuItem.Items.Add(scriptDatabaseDataMenuItem);

            var scriptDatabaseSchemaDataMenuItem = new MenuItem
            {
                Header = "Script SQL Server Database Schema and Data...",
                Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
                ToolTip = toolTip,
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters,
                Tag = SqlCeScripting.Scope.SchemaData 
            };
            scriptDatabaseSchemaDataMenuItem.CommandBindings.Add(scriptDatabaseCommandBinding);
            scriptDatabaseRootMenuItem.Items.Add(scriptDatabaseSchemaDataMenuItem);

            var scriptDatabaseSchemaDataSQLiteMenuItem = new MenuItem
            {
                Header = "Script SQL Server Database Schema and Data for SQLite...",
                Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
                ToolTip = toolTip,
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters,
                Tag = SqlCeScripting.Scope.SchemaDataSQLite
            };
            scriptDatabaseSchemaDataSQLiteMenuItem.CommandBindings.Add(scriptDatabaseCommandBinding);
            scriptDatabaseRootMenuItem.Items.Add(scriptDatabaseSchemaDataSQLiteMenuItem);

            var scriptDatabaseSchemaSQLiteMenuItem = new MenuItem
            {
                Header = "Script SQL Server Database Schema for SQLite...",
                Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
                ToolTip = toolTip,
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters,
                Tag = SqlCeScripting.Scope.SchemaSQLite
            };
            scriptDatabaseSchemaSQLiteMenuItem.CommandBindings.Add(scriptDatabaseCommandBinding);
            scriptDatabaseRootMenuItem.Items.Add(scriptDatabaseSchemaSQLiteMenuItem);

            var scriptDatabaseSchemaDataBLOBMenuItem = new MenuItem
            {
                Header = "Script SQL Server Database Schema and Data with BLOBs...",
                ToolTip = toolTip,
                Icon = ImageHelper.GetImageFromResource("../resources/script_16xLG.png"),
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters,
                Tag = SqlCeScripting.Scope.SchemaDataBlobs 
            };
            scriptDatabaseSchemaDataBLOBMenuItem.CommandBindings.Add(scriptDatabaseCommandBinding);
            scriptDatabaseRootMenuItem.Items.Add(scriptDatabaseSchemaDataBLOBMenuItem);
            Items.Add(scriptDatabaseRootMenuItem);
            Items.Add(new Separator());

            var exportServerCommandBinding = new CommandBinding(DatabaseMenuCommands.DatabaseCommand,
                                                                dcmd.ExportServerDatabaseTo40);
            var exportServerMenuItem = new MenuItem
            {
                Header = "Export SQL Server to SQL Server Compact 4.0...",
                Icon = ImageHelper.GetImageFromResource("../resources/ExportReportData_10565.png"),
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters
            };
            exportServerMenuItem.CommandBindings.Add(exportServerCommandBinding);
            exportServerMenuItem.IsEnabled = (isSqlCe40Installed);
            Items.Add(exportServerMenuItem);

            var exportServerToLiteCommandBinding = new CommandBinding(DatabaseMenuCommands.DatabaseCommand,
                                                               dcmd.ExportServerDatabaseToSqlite);
            var exportServerToLiteMenuItem = new MenuItem
            {
                Header = "Export SQL Server to SQLite... (beta)",
                Icon = ImageHelper.GetImageFromResource("../resources/ExportReportData_10565.png"),
                Command = DatabaseMenuCommands.DatabaseCommand,
                CommandParameter = databaseMenuCommandParameters
            };
            exportServerToLiteMenuItem.CommandBindings.Add(exportServerToLiteCommandBinding);
            Items.Add(exportServerToLiteMenuItem);
        }
    }
}