using System.Data;
using Utilities.MVVM;


namespace DataLogApp.Model
{
    public class StockTuneTables : ObservableObject
    {
        private DataTable _gears12Afr;
        private DataTable _gears34Afr;
        private DataTable _gears56Afr;
        private DataTable _oilMeteringTable;
        private DataTable _leadingIgnitionTimingTable;
        private DataTable _trailingIgnitionTimingTable;
        private DataTable _coilDwellTimingTable;
        private DataTable _mafTable;

        private int _bank1Injectors;
        private int _bank2Injectors;
        private int _bank3Injectors;

        private int _fan1;
        private int _fan2;
        

    }
}