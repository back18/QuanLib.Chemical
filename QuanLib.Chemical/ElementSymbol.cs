using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public enum ElementSymbol
    {
        /// <summary>
        /// 氢 (Hydrogen)
        /// </summary>
        H = 1,

        /// <summary>
        /// 氦 (Helium)
        /// </summary>
        He = 2,

        /// <summary>
        /// 锂 (Lithium)
        /// </summary>
        Li = 3,

        /// <summary>
        /// 铍 (Beryllium)
        /// </summary>
        Be = 4,

        /// <summary>
        /// 硼 (Boron)
        /// </summary>
        B = 5,

        /// <summary>
        /// 碳 (Carbon)
        /// </summary>
        C = 6,

        /// <summary>
        /// 氮 (Nitrogen)
        /// </summary>
        N = 7,

        /// <summary>
        /// 氧 (Oxygen)
        /// </summary>
        O = 8,

        /// <summary>
        /// 氟 (Fluorine)
        /// </summary>
        F = 9,

        /// <summary>
        /// 氖 (Neon)
        /// </summary>
        Ne = 10,

        /// <summary>
        /// 钠 (Sodium)
        /// </summary>
        Na = 11,

        /// <summary>
        /// 镁 (Magnesium)
        /// </summary>
        Mg = 12,

        /// <summary>
        /// 铝 (Aluminum)
        /// </summary>
        Al = 13,

        /// <summary>
        /// 硅 (Silicon)
        /// </summary>
        Si = 14,

        /// <summary>
        /// 磷 (Phosphorus)
        /// </summary>
        P = 15,

        /// <summary>
        /// 硫 (Sulfur)
        /// </summary>
        S = 16,

        /// <summary>
        /// 氯 (Chlorine)
        /// </summary>
        Cl = 17,

        /// <summary>
        /// 氩 (Argon)
        /// </summary>
        Ar = 18,

        /// <summary>
        /// 钾 (Potassium)
        /// </summary>
        K = 19,

        /// <summary>
        /// 钙 (Calcium)
        /// </summary>
        Ca = 20,

        /// <summary>
        /// 钪 (Scandium)
        /// </summary>
        Sc = 21,

        /// <summary>
        /// 钛 (Titanium)
        /// </summary>
        Ti = 22,

        /// <summary>
        /// 钒 (Vanadium)
        /// </summary>
        V = 23,

        /// <summary>
        /// 铬 (Chromium)
        /// </summary>
        Cr = 24,

        /// <summary>
        /// 锰 (Manganese)
        /// </summary>
        Mn = 25,

        /// <summary>
        /// 铁 (Iron)
        /// </summary>
        Fe = 26,

        /// <summary>
        /// 钴 (Cobalt)
        /// </summary>
        Co = 27,

        /// <summary>
        /// 镍 (Nickel)
        /// </summary>
        Ni = 28,

        /// <summary>
        /// 铜 (Copper)
        /// </summary>
        Cu = 29,

        /// <summary>
        /// 锌 (Zinc)
        /// </summary>
        Zn = 30,

        /// <summary>
        /// 镓 (Gallium)
        /// </summary>
        Ga = 31,

        /// <summary>
        /// 锗 (Germanium)
        /// </summary>
        Ge = 32,

        /// <summary>
        /// 砷 (Arsenic)
        /// </summary>
        As = 33,

        /// <summary>
        /// 硒 (Selenium)
        /// </summary>
        Se = 34,

        /// <summary>
        /// 溴 (Bromine)
        /// </summary>
        Br = 35,

        /// <summary>
        /// 氪 (Krypton)
        /// </summary>
        Kr = 36,

        /// <summary>
        /// 铷 (Rubidium)
        /// </summary>
        Rb = 37,

        /// <summary>
        /// 锶 (Strontium)
        /// </summary>
        Sr = 38,

        /// <summary>
        /// 钇 (Yttrium)
        /// </summary>
        Y = 39,

        /// <summary>
        /// 锆 (Zirconium)
        /// </summary>
        Zr = 40,

        /// <summary>
        /// 铌 (Niobium)
        /// </summary>
        Nb = 41,

        /// <summary>
        /// 钼 (Molybdenum)
        /// </summary>
        Mo = 42,

        /// <summary>
        /// 锝 (Technetium)
        /// </summary>
        Tc = 43,

        /// <summary>
        /// 钌 (Ruthenium)
        /// </summary>
        Ru = 44,

        /// <summary>
        /// 铑 (Rhodium)
        /// </summary>
        Rh = 45,

        /// <summary>
        /// 钯 (Palladium)
        /// </summary>
        Pd = 46,

        /// <summary>
        /// 银 (Silver)
        /// </summary>
        Ag = 47,

        /// <summary>
        /// 镉 (Cadmium)
        /// </summary>
        Cd = 48,

        /// <summary>
        /// 铟 (Indium)
        /// </summary>
        In = 49,

        /// <summary>
        /// 锡 (Tin)
        /// </summary>
        Sn = 50,

        /// <summary>
        /// 锑 (Antimony)
        /// </summary>
        Sb = 51,

        /// <summary>
        /// 碲 (Tellurium)
        /// </summary>
        Te = 52,

        /// <summary>
        /// 碘 (Iodine)
        /// </summary>
        I = 53,

        /// <summary>
        /// 氙 (Xenon)
        /// </summary>
        Xe = 54,

        /// <summary>
        /// 铯 (Cesium)
        /// </summary>
        Cs = 55,

        /// <summary>
        /// 钡 (Barium)
        /// </summary>
        Ba = 56,

        /// <summary>
        /// 镧 (Lanthanum)
        /// </summary>
        La = 57,

        /// <summary>
        /// 铈 (Cerium)
        /// </summary>
        Ce = 58,

        /// <summary>
        /// 镨 (Praseodymium)
        /// </summary>
        Pr = 59,

        /// <summary>
        /// 钕 (Neodymium)
        /// </summary>
        Nd = 60,

        /// <summary>
        /// 钷 (Promethium)
        /// </summary>
        Pm = 61,

        /// <summary>
        /// 钐 (Samarium)
        /// </summary>
        Sm = 62,

        /// <summary>
        /// 铕 (Europium)
        /// </summary>
        Eu = 63,

        /// <summary>
        /// 钆 (Gadolinium)
        /// </summary>
        Gd = 64,

        /// <summary>
        /// 铽 (Terbium)
        /// </summary>
        Tb = 65,

        /// <summary>
        /// 镝 (Dysprosium)
        /// </summary>
        Dy = 66,

        /// <summary>
        /// 钬 (Holmium)
        /// </summary>
        Ho = 67,

        /// <summary>
        /// 铒 (Erbium)
        /// </summary>
        Er = 68,

        /// <summary>
        /// 铥 (Thulium)
        /// </summary>
        Tm = 69,

        /// <summary>
        /// 镱 (Ytterbium)
        /// </summary>
        Yb = 70,

        /// <summary>
        /// 镥 (Lutetium)
        /// </summary>
        Lu = 71,

        /// <summary>
        /// 铪 (Hafnium)
        /// </summary>
        Hf = 72,

        /// <summary>
        /// 钽 (Tantalum)
        /// </summary>
        Ta = 73,

        /// <summary>
        /// 钨 (Tungsten)
        /// </summary>
        W = 74,

        /// <summary>
        /// 铼 (Rhenium)
        /// </summary>
        Re = 75,

        /// <summary>
        /// 锇 (Osmium)
        /// </summary>
        Os = 76,

        /// <summary>
        /// 铱 (Iridium)
        /// </summary>
        Ir = 77,

        /// <summary>
        /// 铂 (Platinum)
        /// </summary>
        Pt = 78,

        /// <summary>
        /// 金 (Gold)
        /// </summary>
        Au = 79,

        /// <summary>
        /// 汞 (Mercury)
        /// </summary>
        Hg = 80,

        /// <summary>
        /// 铊 (Thallium)
        /// </summary>
        Tl = 81,

        /// <summary>
        /// 铅 (Lead)
        /// </summary>
        Pb = 82,

        /// <summary>
        /// 铋 (Bismuth)
        /// </summary>
        Bi = 83,

        /// <summary>
        /// 钋 (Polonium)
        /// </summary>
        Po = 84,

        /// <summary>
        /// 砹 (Astatine)
        /// </summary>
        At = 85,

        /// <summary>
        /// 氡 (Radon)
        /// </summary>
        Rn = 86,

        /// <summary>
        /// 钫 (Francium)
        /// </summary>
        Fr = 87,

        /// <summary>
        /// 镭 (Radium)
        /// </summary>
        Ra = 88,

        /// <summary>
        /// 锕 (Actinium)
        /// </summary>
        Ac = 89,

        /// <summary>
        /// 钍 (Thorium)
        /// </summary>
        Th = 90,

        /// <summary>
        /// 镤 (Protactinium)
        /// </summary>
        Pa = 91,

        /// <summary>
        /// 铀 (Uranium)
        /// </summary>
        U = 92,

        /// <summary>
        /// 镎 (Neptunium)
        /// </summary>
        Np = 93,

        /// <summary>
        /// 钚 (Plutonium)
        /// </summary>
        Pu = 94,

        /// <summary>
        /// 镅 (Americium)
        /// </summary>
        Am = 95,

        /// <summary>
        /// 锔 (Curium)
        /// </summary>
        Cm = 96,

        /// <summary>
        /// 锫 (Berkelium)
        /// </summary>
        Bk = 97,

        /// <summary>
        /// 锎 (Californium)
        /// </summary>
        Cf = 98,

        /// <summary>
        /// 锿 (Einsteinium)
        /// </summary>
        Es = 99,

        /// <summary>
        /// 镄 (Fermium)
        /// </summary>
        Fm = 100,

        /// <summary>
        /// 钔 (Mendelevium)
        /// </summary>
        Md = 101,

        /// <summary>
        /// 锘 (Nobelium)
        /// </summary>
        No = 102,

        /// <summary>
        /// 铹 (Lawrencium)
        /// </summary>
        Lr = 103,

        /// <summary>
        /// 𬬻 (Rutherfordium)
        /// </summary>
        Rf = 104,

        /// <summary>
        /// 𬭊 (Dubnium)
        /// </summary>
        Db = 105,

        /// <summary>
        /// 𬭳 (Seaborgium)
        /// </summary>
        Sg = 106,

        /// <summary>
        /// 𬭛 (Bohrium)
        /// </summary>
        Bh = 107,

        /// <summary>
        /// 𬭶 (Hassium)
        /// </summary>
        Hs = 108,

        /// <summary>
        /// 鿏 (Meitnerium)
        /// </summary>
        Mt = 109,

        /// <summary>
        /// 𫟼 (Darmstadtium)
        /// </summary>
        Ds = 110,

        /// <summary>
        /// 𬬭 (Roentgenium)
        /// </summary>
        Rg = 111,

        /// <summary>
        /// 鿔 (Copernicium)
        /// </summary>
        Cn = 112,

        /// <summary>
        /// 鿭 (Nihonium)
        /// </summary>
        Nh = 113,

        /// <summary>
        /// 𫓧 (Flerovium)
        /// </summary>
        Fl = 114,

        /// <summary>
        /// 镆 (Moscovium)
        /// </summary>
        Mc = 115,

        /// <summary>
        /// 𫟷 (Livermorium)
        /// </summary>
        Lv = 116,

        /// <summary>
        /// 鿬 (Tennessine)
        /// </summary>
        Ts = 117,

        /// <summary>
        /// 鿫 (Oganesson)
        /// </summary>
        Og = 118,

    }
}
