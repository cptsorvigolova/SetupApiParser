using SetupApiParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetupApiParser
{
    public static class Constants
    {
        public static Regex DateTimeRegex = new Regex(@"\d{4}/\d\d/\d\d \d\d:\d\d:\d\d\.\d{2,3}");
        public static Regex SectionRegex = new Regex(@"\n(.+\n){4,}\n");
        public static Regex BootSessionHeaderRegex = new Regex($"\\[Boot Session: {DateTimeRegex}]");
        public static Regex BootSessionRegex = new Regex($"\n{BootSessionHeaderRegex}\n({SectionRegex})*");

        public const string SetupApiStart = "[Device Install Log]";
        public const string SetupApiBeginLog = "[BeginLog]";
    }

    public static class Mapping
    {
        public static Dictionary<string, EntryPrefix> EntryPrefixes = new Dictionary<string, EntryPrefix>
        {
            {"!!! ", EntryPrefix.Error },
            {"!   ", EntryPrefix.Warning },
            {"    ", EntryPrefix.Info }
        };

        public static Dictionary<string, EventCategory> EventCategories = new Dictionary<string, EventCategory>
        {
            {"...: ", EventCategory.Vendor_supplied_operation },
            {"bak: ", EventCategory.Backup_data },
            {"cci: ", EventCategory.Class_installer_or_co_installer_operation },
            {"cpy: ", EventCategory.Copy_files },
            {"dvi: ", EventCategory.Device_installation },
            {"flq: ", EventCategory.Manage_file_queues },
            {"inf: ", EventCategory.Manage_INF_files },
            {"ndv: ", EventCategory.New_device_wizard },
            {"prp: ", EventCategory.Manage_device_and_driver_properties },
            {"reg: ", EventCategory.Manage_registry_settings },
            {"set: ", EventCategory.General_setup },
            {"sig: ", EventCategory.Verify_digital_signatures }, 
            {"sto: ", EventCategory.Manage_the_driver_store },
            {"ui : ", EventCategory.Manage_user_interface_dialog_boxes },
            {"ump: ", EventCategory.User_mode_PnP_manager }
        };

        public static Dictionary<EventCategory, string> EventCategoriesDescription = new Dictionary<EventCategory, string>
        {
            {EventCategory.Vendor_supplied_operation, "Vendor-supplied operation" },
            {EventCategory.Backup_data, "Backup data" },
            {EventCategory.Class_installer_or_co_installer_operation, "Class installer or co-installer operation" },
            {EventCategory.Copy_files, "Copy files" },
            {EventCategory.Device_installation, "Device installation" },
            {EventCategory.Manage_file_queues, "Manage file queues" },
            {EventCategory.Manage_INF_files, "Manage INF files" },
            {EventCategory.New_device_wizard, "New device wizard" },
            {EventCategory.Manage_device_and_driver_properties, "Manage device and driver properties" },
            {EventCategory.Manage_registry_settings, "Manage registry settings" },
            {EventCategory.General_setup, "General setup" },
            {EventCategory.Verify_digital_signatures, "Verify digital signatures" },
            {EventCategory.Manage_the_driver_store, "Manage the driver store" },
            {EventCategory.Manage_user_interface_dialog_boxes, "Manage user interface dialog boxes" },
            {EventCategory.User_mode_PnP_manager, "User-mode PnP manager" }
        };
    }
}
