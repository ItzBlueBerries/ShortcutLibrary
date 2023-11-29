using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Shortcut
{
    /// <summary>
    /// Starmail related methods, this includes creating new mail.
    /// </summary>
    public static class Mail
    {
        /// <summary>
        /// Sends mail to the player if it exist.
        /// </summary>
        /// <param name="mailKey">The key <see cref="string"/> for the mail.</param>
        /// <param name="mailType">The <see cref="MailDirector.Type"/> of the mail.</param>
        /// <returns><see cref="bool"/></returns>
        public static bool SendMail(string mailKey, MailDirector.Type mailType) => Director.Mail.SendMailIfExists(mailType, mailKey);

        /// <summary>
        /// Creates a new <see cref="MailRegistry.MailEntry"/>.
        /// </summary>
        /// <param name="mailKey">The key <see cref="string"/> of this mail entry.</param>
        /// <param name="mailFrom">The sender <see cref="string"/> of this mail entry.</param>
        /// <param name="mailSubject">The subject <see cref="string"/> of this mail entry.</param>
        /// <param name="mailBody">The body <see cref="string"/> of this mail entry.</param>
        /// <param name="readCallback">The callback delegate <see cref="Action"/> for when the mail is read.</param>
        /// <returns><see cref="MailRegistry.MailEntry"/></returns>
        public static MailRegistry.MailEntry CreateStarmail(string mailKey, string mailFrom, string mailSubject, string mailBody, [Optional] Action<MailDirector, MailDirector.Mail> readCallback)
        {
            if (readCallback == null)
                readCallback = delegate { };

            MailRegistry.MailEntry mailEntry = new MailRegistry.MailEntry(mailKey);
            MailRegistry.RegisterMailEntry(mailEntry)
                .SetFromTranslation(mailFrom)
                .SetSubjectTranslation(mailSubject)
                .SetBodyTranslation(mailBody)
                .SetReadCallback(readCallback);

            return mailEntry;
        }
    }
}
