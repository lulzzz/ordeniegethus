using System;

namespace Arkitektum.Orden.Models
{
    public class LicenseDocument
    {
        public static int Id { get; set; }

        ///<summary>
        /// Refererer til en type lisens, f.eks "åpen lisens" eller "royalties kreves".
        /// </summary>
        public static String LicenseType { get; set; }

    }
}