using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser.Model
{
    public enum EntryPrefix
    {
        Unknown,
        Error,
        Warning,
        Info
    }
    public enum EventCategory
    {
        Unknown,
        Vendor_supplied_operation,
        Backup_data,
        Class_installer_or_co_installer_operation,
        Copy_files,
        Device_installation,
        Manage_file_queues,
        Manage_INF_files,
        New_device_wizard,
        Manage_device_and_driver_properties,
        Manage_registry_settings,
        General_setup,
        Verify_digital_signatures,
        Manage_the_driver_store,
        Manage_user_interface_dialog_boxes,
        User_mode_PnP_manager
    }
}
