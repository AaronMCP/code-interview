using Hys.Consultation.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Services
{
    public interface IShortcutService
    {
        ShortcutDto AddShortcut(ShortcutDto shortcut);
        ShortcutDto GetShortcut(string shortcutID);
        ShortcutDto UpdateShortcut(ShortcutDto shortcut);
        void DeleteShortcut(string shortcutID);
        IEnumerable<ShortcutDto> GetShortcuts(string userID, ShortcutCategory category);
    }
}
