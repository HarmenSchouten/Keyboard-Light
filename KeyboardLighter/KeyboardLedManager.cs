using HidLibrary;
using KeyboardLighter.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyboardLighter
{
    class KeyboardLedManager
    {
        private const int unkown_vendor = 1739;//2321; //16700     //1739  //2321  //
        private const int unknown_product = 30143;//8584; //43982    //30143 //8584  //

        private const int mouse_vendor = 1241;
        private const int mouse_product = 41118;

        private const int keyboard_vendor = 2321;
        private const int keyboard_product = 8584;

        public static KeyboardLedManager instance;
        public KeyboardLedManager()
        {
            Thread checkNewDevicesThread = new Thread(() => checkNewDevices());
            checkNewDevicesThread.IsBackground = true;
            checkNewDevicesThread.Start();
        }

        public void Start()
        {
            Thread checkNewDevicesThread = new Thread(() => checkNewDevices());
            checkNewDevicesThread.IsBackground = true;
            checkNewDevicesThread.Start();
        }

        public void checkNewDevices()
        {
            while (true)
            {
                bool newDevice = false;
                List<HidDevice> devices_6_new = new List<HidDevice>();
                List<HidDevice> devices_257_new = new List<HidDevice>();
                List<HidDevice> devices_8_new = new List<HidDevice>();
                List<HidDevice> devices_65_new = new List<HidDevice>();

                IEnumerable<HidDevice> devices = HidDevices.Enumerate(keyboard_vendor, keyboard_product);
                List<HidDevice> devices_list = devices.ToList<HidDevice>();
                foreach (HidDevice dev in devices_list)
                {
                    Console.WriteLine(dev.Capabilities.FeatureReportByteLength);
                    if (dev.Capabilities.FeatureReportByteLength == 8)
                    {
                        //_device_8 = dev;
                        devices_8_new.Add(dev);
                        bool alreadyExists = false;

                        foreach (HidDevice existingDev in devices_8)
                        {
                            if (existingDev.DevicePath.Equals(dev.DevicePath))
                            {
                                alreadyExists = true;
                            }
                        }

                        if (!alreadyExists)
                        {
                            newDevice = true;
                            Console.WriteLine("New device is found");
                            devices_8.Add(dev);
                        }
                    }

                    if (dev.Capabilities.FeatureReportByteLength == 65)
                    {
                        //_device_65 = dev;
                        devices_65_new.Add(dev);
                        bool alreadyExists = false;

                        foreach (HidDevice existingDev in devices_65)
                        {
                            if (existingDev.DevicePath.Equals(dev.DevicePath))
                            {
                                alreadyExists = true;
                            }
                        }

                        if (!alreadyExists)
                        {
                            newDevice = true;
                            Console.WriteLine("New device is found");
                            devices_65.Add(dev);
                        }
                    }

                    if (dev.Capabilities.FeatureReportByteLength == 6)
                    {
                        devices_6_new.Add(dev);
                        bool alreadyExists = false;

                        foreach (HidDevice existingDev in devices_6)
                        {
                            if (existingDev.DevicePath.Equals(dev.DevicePath))
                            {
                                alreadyExists = true;
                            }
                        }
                        if (alreadyExists == false)
                        {
                            newDevice = true;
                            Console.WriteLine("New device is found");
                            devices_6.Add(dev);
                        }
                    }
                    if (dev.Capabilities.FeatureReportByteLength == 0)
                    {
                        devices_257_new.Add(dev);
                        bool alreadyExists = false;

                        foreach (HidDevice existingDev in devices_257)
                        {
                            if (existingDev.DevicePath.Equals(dev.DevicePath))
                            {
                                alreadyExists = true;
                            }
                        }
                        if (alreadyExists == false)
                        {
                            newDevice = true;
                            Console.WriteLine("New device is found");
                            devices_257.Add(dev);
                        }
                    }
                }
                if (newDevice == true)
                {
                    updateMouseIndicator(Program.indicatorModes.RED_ON);
                }
                else
                {
                    updateMouseIndicator(Program.indicatorModes.ORANGE_ON);
                }

                devices_6 = devices_6_new;
                devices_8 = devices_8_new;
                devices_65 = devices_65_new;
                devices_257 = devices_257_new;

                Thread.Sleep(1000);
            }
        }

        private List<HidDevice> devices_6 = new List<HidDevice>();
        private List<HidDevice> devices_8 = new List<HidDevice>();
        private List<HidDevice> devices_65 = new List<HidDevice>();
        private List<HidDevice> devices_257 = new List<HidDevice>();

        private volatile bool settingColour;
        private volatile bool ledRecentlyChanged = false;
        private System.Timers.Timer ledTimer = new System.Timers.Timer(2000);
        private System.Timers.Timer mouseUpdateTimer = new System.Timers.Timer(2000);
        private Program.indicatorModes currentMode = Program.indicatorModes.RED_ON;
        private Program.indicatorModes newMode = Program.indicatorModes.RED_ON;

        public static KeyboardLedManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KeyboardLedManager();
                }
                return instance;
            }
        }

        private static byte[] GenerateLedSettingPacket(bool glow)
        {
            return GenerateLedSettingPacket(glow, 4);
        }

        private static byte[] GenerateLedSettingPacket(bool glow, int seconds)
        {
            byte[] data = new byte[40];

            data = StringToByteArray("F0EE750FFB47B757CD6B000000D525470A1F7B867F4432D7016FE618E4D8A157B56E2C0AAC4B53D7E2EE038A82CB74D78A6EBE0A3DCBA5D6236F650BE54A26D6");

            byte[] repl = new byte[3];
            if (!glow)
            {
                repl = StringToByteArray("F78E76");
            }
            else
            {
                repl = StringToByteArray("F608F6");
                BitArray b = new BitArray(repl);
                switch (seconds)
                {
                    case 3:
                        b.Set(9, false);
                        b.Set(8, true);
                        b.Set(7, false);
                        break;
                    case 4:
                        b.Set(9, false);
                        b.Set(8, false);
                        b.Set(7, true);
                        break;
                    case 5:
                        b.Set(9, true);
                        b.Set(8, true);
                        b.Set(7, true);
                        break;
                    case 6:
                        b.Set(9, true);
                        b.Set(8, true);
                        b.Set(7, true);
                        break;
                    case 7:
                        b.Set(9, true);
                        b.Set(8, false);
                        b.Set(7, false);
                        break;
                    case 8:
                        b.Set(9, true);
                        b.Set(8, false);
                        b.Set(7, true);
                        break;
                    default:
                        b.Set(9, false);
                        b.Set(8, false);
                        b.Set(7, false);
                        break;
                }
                b.CopyTo(repl, 0);
            }

            Array.ConstrainedCopy(repl, 0, data, 10, 3);

            return data;
        }

        private static byte[] GenerateColourPacket(byte red, byte green, byte blue)
        {
            byte[] data = new byte[40];

            data = StringToByteArray("126EF70BF7CB3553D96C385AF54BB5D600EEF50B73493A5600000000000000000000000077CB3557006E770AF7CB3557E06D070707C7C555E0E7070207C04559");

            byte maskR1 = 0xEE, maskR2 = 0x96, maskR3 = 0x00, maskR4 = 0x15;
            byte maskG1 = 0xDC, maskG2 = 0xEF, maskG3 = 0xAE, maskG4 = 0xEE;
            byte maskB1 = 0x00, maskB2 = 0x15, maskB3 = 0x6A, maskB4 = 0xDC;

            byte R1 = (byte)(red ^ maskR1), R2 = (byte)(red ^ maskR2), R3 = (byte)(red ^ maskR3), R4 = (byte)(red ^ maskR4);
            byte G1 = (byte)(green ^ maskG1), G2 = (byte)(green ^ maskG2), G3 = (byte)(green ^ maskG3), G4 = (byte)(green ^ maskG4);
            byte B1 = (byte)(blue ^ maskB1), B2 = (byte)(blue ^ maskB2), B3 = (byte)(blue ^ maskB3), B4 = (byte)(blue ^ maskB4);

            byte[] colourData = new byte[13];
            colourData[0] |= B1;

            colourData[1] |= (byte)(G1 >> 1);
            colourData[1] |= (byte)(G3 << 7);

            colourData[2] |= (byte)(G1 << 7);
            colourData[2] |= (byte)(R1 >> 1);

            colourData[3] |= (byte)(R1 << 7);
            colourData[3] |= (byte)(B2 >> 1);

            colourData[4] |= (byte)(B2 << 7);
            colourData[4] |= (byte)(G2 >> 1);

            colourData[5] |= (byte)(G2 << 7);
            colourData[5] |= (byte)(R2 >> 1);

            colourData[6] |= (byte)(R2 << 7);
            colourData[6] |= (byte)(B3 >> 1);

            colourData[7] |= (byte)(B3 << 7);
            colourData[7] |= (byte)(G3 >> 1);

            colourData[8] |= R3;

            colourData[9] |= (byte)(B4 >> 1);

            colourData[10] |= (byte)(B4 << 7);
            colourData[10] |= (byte)(G4 >> 1);


            colourData[11] |= (byte)(G4 << 7);
            colourData[11] |= (byte)(R4 >> 1);

            colourData[12] |= (byte)(R4 << 7);

            Array.ConstrainedCopy(colourData, 0, data, 24, 13);

            return data;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public void updateMouseIndicator(Program.indicatorModes mode)
        {
            bool mousePressed = false;

            new Thread(() =>
            {
                if (settingColour) return;
                settingColour = true;

                mouseUpdateTimer.Stop();

                byte r = 0, g = 0, b = 0;
                bool respiration = false;
                int seconds = 4;

                switch (mode)
                {
                    case Program.indicatorModes.GREEN_FAST:
                    case Program.indicatorModes.GREEN_GLOW:
                    case Program.indicatorModes.GREEN_ON:
                    case Program.indicatorModes.GREEN_SLOW:
                        Console.WriteLine("CASE 1");
                        r = Settings.Default.ColorGreenR; g = Settings.Default.ColorGreenG; b = Settings.Default.ColorGreenB;
                        break;
                    case Program.indicatorModes.ORANGE_FAST:
                    case Program.indicatorModes.ORANGE_GLOW:
                    case Program.indicatorModes.ORANGE_ON:
                    case Program.indicatorModes.ORANGE_SLOW:
                        Console.WriteLine("CASE 2");
                        r = Settings.Default.ColorOrangeR; g = Settings.Default.ColorOrangeG; b = Settings.Default.ColorOrangeB;
                        break;
                    case Program.indicatorModes.RED_FAST:
                    case Program.indicatorModes.RED_GLOW:
                    case Program.indicatorModes.RED_ON:
                    case Program.indicatorModes.RED_SLOW:
                        Console.WriteLine("CASE 3");
                        r = Settings.Default.ColorRedR; g = Settings.Default.ColorRedG; b = Settings.Default.ColorRedB;
                        break;
                }

                switch (mode)
                {
                    case Program.indicatorModes.GREEN_ON:
                    case Program.indicatorModes.ORANGE_ON:
                    case Program.indicatorModes.RED_ON:
                        Console.WriteLine("CASE 4");
                        respiration = false;
                        seconds = 4;
                        break;
                    case Program.indicatorModes.GREEN_FAST:
                    case Program.indicatorModes.ORANGE_FAST:
                    case Program.indicatorModes.RED_FAST:
                        Console.WriteLine("CASE 5");
                        respiration = true;
                        seconds = 3;
                        break;
                    case Program.indicatorModes.GREEN_SLOW:
                    case Program.indicatorModes.ORANGE_SLOW:
                    case Program.indicatorModes.RED_SLOW:
                        Console.WriteLine("CASE 6");
                        respiration = true;
                        seconds = 5;
                        break;
                    case Program.indicatorModes.GREEN_GLOW:
                    case Program.indicatorModes.ORANGE_GLOW:
                    case Program.indicatorModes.RED_GLOW:
                        Console.WriteLine("CASE 8");
                        respiration = true;
                        seconds = 8;
                        break;
                }
               
                if (devices_6.Count == 0)
                {
                    settingColour = false;
                    currentMode = mode;
                    return;
                }
                Console.WriteLine(devices_6.Count);

                for (int devidx = 0; devidx < devices_6.Count; devidx++)
                {
                    for (int i = 0; i < 2; i++)
                    { 
                        byte[] _data_8;

                        devices_6[devidx].WriteFeatureData(StringToByteArray("013B117D99493557"));
                        devices_6[devidx].ReadFeatureData(out _data_8, 1);
                        byte[] ledSettingData = new byte[65];
                        GenerateLedSettingPacket(respiration, seconds).CopyTo(ledSettingData, 1);
                        ledSettingData[0] = 0;
                        devices_6[devidx].WriteFeatureData(ledSettingData);
                        Thread.Sleep(20);

                        devices_6[devidx].WriteFeatureData(StringToByteArray("013B117D9949B557"));
                        devices_6[devidx].ReadFeatureData(out _data_8, 1);
                        byte[] colorData = new byte[65];
                        GenerateColourPacket(r, g, b).CopyTo(colorData, 1);
                        colorData[0] = 0;
                        devices_6[devidx].WriteFeatureData(colorData);
                        Thread.Sleep(20);
                    }
                }

                settingColour = false;
                currentMode = mode;
                ledRecentlyChanged = true;
                ledTimer.Stop();
                ledTimer.Start();
            }).Start();
        }
    }
}