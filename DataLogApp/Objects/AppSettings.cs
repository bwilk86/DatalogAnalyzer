using System;
using System.Collections.Generic;
using DataLogApp.Properties;

namespace DataLogApp.Objects
{
    public class AppSettings
    {
        public Dictionary<string, ColumnNames> ColumnNames;

        public AppSettings()
        {
            ColumnNames = GetColumnNamesDictionaryFromResources();
        }
        private Dictionary<string, ColumnNames> GetColumnNamesDictionaryFromResources()
        {
            var colDictionary = new Dictionary<string, ColumnNames> { { "Cobb", GetCobbColumnNames() } };

            return colDictionary;
        }

        private ColumnNames GetCobbColumnNames()
        {
            var cobbColumnNamesArray = Resources.Cobb.Split(Convert.ToChar(";"));
            var cobbColumnNames = new ColumnNames()
            {
                Time = cobbColumnNamesArray[0],
                AccelPedalPos = cobbColumnNamesArray[1],
                BatteryVoltage = cobbColumnNamesArray[2],
                CoolantTemp = cobbColumnNamesArray[3],
                EquivRatio = cobbColumnNamesArray[4],
                InjPulseWidth = cobbColumnNamesArray[5],
                Rpm = cobbColumnNamesArray[6],
                InjDutyCycle = cobbColumnNamesArray[7],
                IntakeTemp = cobbColumnNamesArray[8],
                KnockRetard = cobbColumnNamesArray[9],
                CalculatedLoad = cobbColumnNamesArray[10],
                LongTerm = cobbColumnNamesArray[11],
                MassAirflowRate = cobbColumnNamesArray[12],
                MAFVoltage = cobbColumnNamesArray[13],
                ShortTerm = cobbColumnNamesArray[14],
                ThrottlePosition = cobbColumnNamesArray[15],
                VehicleSpeed = cobbColumnNamesArray[16],
                MetOilPumpPos = cobbColumnNamesArray[17],
                IgnTimLeadCoil = cobbColumnNamesArray[18],
                IgnTimTrailCoil = cobbColumnNamesArray[19],
                IgnSeparation = cobbColumnNamesArray[20]
            };

            return cobbColumnNames;
        }
    }
}
