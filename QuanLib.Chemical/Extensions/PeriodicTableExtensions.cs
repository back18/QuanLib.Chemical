using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.Extensions
{
    public static class PeriodicTableExtensions
    {
        public static ElectronConfiguration GetAllElectronConfiguration(this Element element)
        {
            ArgumentNullException.ThrowIfNull(element, nameof(element));

            List<Orbital> orbitals = [];
            ElectronConfiguration electronConfiguration = element.ElectronConfiguration;
            orbitals.AddRange(electronConfiguration.Orbitals);

            if (electronConfiguration.BasedOn.HasValue)
            {
                ElectronConfiguration baseConfiguration = GetAllElectronConfiguration(PeriodicTable.GetElement(electronConfiguration.BasedOn.Value));
                foreach (Orbital orbital in baseConfiguration.Orbitals)
                {
                    Orbital[] whereResult = orbitals.Where(i => i.Shell == orbital.Shell && i.Type == orbital.Type).ToArray();
                    if (whereResult.Length > 0)
                    {
                        Orbital merge = orbital;
                        foreach (Orbital item in whereResult)
                        {
                            merge = Orbital.Merge(merge, item);
                            orbitals.Remove(item);
                        }
                        orbitals.Add(merge);
                    }
                    else
                    {
                        orbitals.Add(orbital);
                    }
                }
            }
            return new(null, orbitals);
        }

        public static int[] GetElectronsPerShell(this Element element)
        {
            ArgumentNullException.ThrowIfNull(element, nameof(element));

            Dictionary<int, int> result = [];
            ElectronConfiguration electronConfiguration = GetAllElectronConfiguration(element);

            foreach (Orbital orbital in electronConfiguration.Orbitals)
            {
                result.TryAdd(orbital.Shell, 0);
                result[orbital.Shell] += orbital.Count;
            }

            return result.OrderBy(i => i.Key).Select(i => i.Value).ToArray();
        }

        public static int GetShellCount(this Element element)
        {
            return GetElectronsPerShell(element).Length;
        }

        public static int GetElectronCount(this Element element)
        {
            return GetElectronsPerShell(element).Sum();
        }

        public static int GetProtonCount(this Element element)
        {
            return element.AtomicNumber;
        }

        public static int GetNeutronCount(this Element element)
        {
            return GetStableIsotope(element).MassNumber - element.AtomicNumber;
        }

        public static Isotope GetStableIsotope(this Element element)
        {
            Isotope[] isotopes = PeriodicTable.GetIsotopes(element.Symbol);

            if (isotopes.Length == 0)
                throw new InvalidOperationException();

            if (isotopes.Length == 1)
                return isotopes[0];

            if (isotopes.Where(i => i.Abundance != 0).Any())
                return isotopes.MaxBy(i => i.Abundance)!;

            (Isotope isotope, double offset)[] values = new (Isotope isotope, double offset)[isotopes.Length];
            for (int i = 0; i < isotopes.Length; i++)
            {
                values[i].isotope = isotopes[i];
                values[i].offset = Math.Abs(element.AtomicMass - isotopes[i].AtomicMass);
            }

            return values.MinBy(i => i.offset).isotope;
        }
    }
}
