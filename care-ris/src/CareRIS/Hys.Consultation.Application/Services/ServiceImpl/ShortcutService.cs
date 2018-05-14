using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class ShortcutService : IShortcutService
    {
        private readonly IShortcutRepository _shortcutRepository;

        public ShortcutService(IShortcutRepository shortcutRepository)
        {
            _shortcutRepository = shortcutRepository;
        }

        public ShortcutDto AddShortcut(ShortcutDto shortcut)
        {
            var existedShortcut = _shortcutRepository.Get(s => s.Owner.Equals(shortcut.Owner, StringComparison.OrdinalIgnoreCase)
                    && s.Category == (int)shortcut.Category
                    && s.Name.Equals(shortcut.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (existedShortcut != null)
            {
                if (!shortcut.IgnoreDuplicatedName)
                {
                    throw new DuplicateNameException();
                }
                else
                {
                    _shortcutRepository.Delete(existedShortcut);
                }
            }

            _shortcutRepository.Add(Mapper.Map<ShortcutDto, Shortcut>(shortcut));
            _shortcutRepository.SaveChanges();

            return GetShortcut(shortcut.UniqueId);
        }

        public ShortcutDto GetShortcut(string shortcutID)
        {
            var shortcut = _shortcutRepository
                .Get(s => s.UniqueID.Equals(shortcutID.ToString()))
                .FirstOrDefault();
            if (shortcut != null)
            {
                var result = Mapper.Map<Shortcut, ShortcutDto>(shortcut);
                return result;
            }
            return null;
        }

        public void DeleteShortcut(string shortcutID)
        {
            var shortcut = _shortcutRepository
                .Get(s => s.UniqueID.Equals(shortcutID))
                .FirstOrDefault();
            if (shortcut != null)
            {
                _shortcutRepository.Delete(shortcut);
                _shortcutRepository.SaveChanges();
            }
        }

        public ShortcutDto UpdateShortcut(ShortcutDto shortcut)
        {
            var existingShortcut = _shortcutRepository
                .Get(s => s.UniqueID.Equals(shortcut.UniqueId))
                .FirstOrDefault();

            if (shortcut == null)
            {
                throw new InvalidOperationException("shortcut does not exist.");
            }

            if (shortcut.IsDefault != existingShortcut.IsDefault)
            {
                SetDetaultShortcut(existingShortcut);
            }
            else
            {
                // TODO: do a normal update operation
            }

            return _shortcutRepository.Get(s => s.UniqueID == shortcut.UniqueId).Select(s => Mapper.Map<ShortcutDto>(s)).First();
        }

        private ShortcutDto SetDetaultShortcut(Shortcut shortcut)
        {
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }

            var userID = shortcut.Owner;
            var originals = _shortcutRepository
                .Get(s => s.Owner.Equals(userID) && s.IsDefault == true);
            foreach (var original in originals)
            {
                original.IsDefault = false;
            }
            shortcut.IsDefault = true;
            _shortcutRepository.SaveChanges();

            return GetShortcut(shortcut.UniqueID);
        }

        public IEnumerable<ShortcutDto> GetShortcuts(string userID, ShortcutCategory category)
        {
            var res = _shortcutRepository
           .Get(s => s.Owner.Equals(userID) && s.Category == (int)category);

            var shortcuts = _shortcutRepository
                .Get(s => s.Owner.Equals(userID) && s.Category == (int)category)
                .Select(Mapper.Map<Shortcut, ShortcutDto>).OrderBy(s => s.Name).ToList();

       
            return shortcuts;
        }
    }
}
