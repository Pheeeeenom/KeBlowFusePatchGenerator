using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeBlowFusePatchGenerator.Classes;
namespace KeBlowFusePatchGenerator
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr RoundCorner(
           int leftRect,
           int topRect,
           int rightRect,
           int bottomRect,
           int widthEllipse,
           int heighEllipse
       );
        public Form1()
        {
            InitializeComponent();
        }
        private List<byte> _FuseNibbles = new List<byte>(Constants.FuseNibblesCount);

        private void Form1_Load(object sender, EventArgs e)
        {
            Region = Region.FromHrgn(RoundCorner(0, 0, Width, Height, 21, 21));
            #region Defaults
            cmMotherboard.SelectedItem = "Xenon";
            cmImageType.SelectedItem = "JTAG";
            cmConsoleType.SelectedItem = "Phat Retail";
            #endregion


        }
        int cb;
        int updseq;
        string consoleType;
        string cpukey;
        string motherboard;
        string hacktype;
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result;
                motherboard = cmMotherboard.Text;
                hacktype = cmImageType.Text;

                cb = Int32.Parse(txtCB.Text);
                cpukey = txtCPUKey.Text;

                consoleType = cmConsoleType.Text;

                if (motherboard != "Xenon" &&
                    motherboard != "Zephyr" &&
                    motherboard != "Falcon" &&
                    motherboard != "Jasper" &&
                    motherboard != "Trinity" && motherboard != "Corona" && motherboard != "CoronaWB" && motherboard != "Corona4GB")
                {
                    MessageBox.Show("Pick a valid motherboard.", "Can't", MessageBoxButtons.OK);

                }
                else
                {
                    if (hacktype != "JTAG" &&
                        hacktype != "Glitch" &
                        hacktype != "Glitch2" &&
                        hacktype != "Glitch2m" &&
                        hacktype != "DevGL")
                    {
                        MessageBox.Show("Pick a valid hack type.", "Can't", MessageBoxButtons.OK);

                    }
                    else
                    {
                        if (consoleType != "Phat Retail" &&
                            consoleType != "Slim Retail" &&
                            consoleType != "Development Kit")
                        {
                            MessageBox.Show("Pick a valid console type.", "Can't", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (Utilities.VerifyCPUKey(Utilities.HexStringToByteArray(cpukey)))
                            {
                                if (cb == 0)
                                {
                                    try
                                    {
                                        int updseq = Int32.Parse(txtUpdateSeq.Text);

                                        if (updseq > 80)
                                        {
                                            MessageBox.Show("Over 80 on update LDV is not possible", "Update LDV too high", MessageBoxButtons.OK);
                                        }
                                        else if (updseq < 0)
                                        {
                                            MessageBox.Show("Under 0 on update LDV is not possible", "Update LDV too low", MessageBoxButtons.OK);
                                        }
                                        else
                                        {
                                            CalculateFuses();
                                            GetFuseBitsToBlow();
                                            CreatePatch();
                                        }
                                    }
                                    catch (FormatException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                }
                                else if (cb > 0)
                                {

                                    result = MessageBox.Show("It seems that you have entered a non-zero CB, this means you are potientally blocking certain exploits depending on this value.\nIs this okay?", "NON-ZERO CB", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes && cb > 16)
                                    {
                                        result = MessageBox.Show("It seems that you have entered a CB greater than 16, this isn't possible", "CB greater than what's possible", MessageBoxButtons.OK);

                                    }
                                    if (result == DialogResult.Yes && cb > 12)
                                    {
                                        result = MessageBox.Show("It seems that you have entered a CB greater than 12, this means your console will never boot.\nIs this okay? (HIGHLY SUGGESTED YOU CLICK NO)", "NON-ZERO CB", MessageBoxButtons.YesNo);
                                        if (result == DialogResult.Yes)
                                        {
                                            try
                                            {
                                                updseq = Int32.Parse(txtUpdateSeq.Text);

                                                if (updseq > 80)
                                                {
                                                    MessageBox.Show("Over 80 on update LDV is not possible", "Update LDV too high", MessageBoxButtons.OK);
                                                }
                                                else if (updseq < 0)
                                                {
                                                    MessageBox.Show("Under 0 on update LDV is not possible", "Update LDV too low", MessageBoxButtons.OK);
                                                }
                                                else
                                                {
                                                    CalculateFuses();
                                                    GetFuseBitsToBlow();
                                                    CreatePatch();

                                                }
                                            }
                                            catch (FormatException ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }


                                        }
                                        else
                                        {
                                            //do nothing
                                        }
                                    }
                                   if (result == DialogResult.Yes && cb > 0 && cb <= 12)
                                    {
                                        try
                                        {
                                            int updseq = Int32.Parse(txtUpdateSeq.Text);

                                            if (updseq > 80 || updseq < 0)
                                            {
                                                MessageBox.Show("Over 80 on update LDV is not possible", "Update LDV too high", MessageBoxButtons.OK);
                                            }
                                            else if (updseq < 0)
                                            {
                                                MessageBox.Show("Under 0 on update LDV is not possible", "Update LDV too low", MessageBoxButtons.OK);
                                            }
                                            else
                                            {
                                                CalculateFuses();
                                                GetFuseBitsToBlow();
                                                CreatePatch();

                                            }
                                        }
                                        catch (FormatException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                    else
                                    {
                                      
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Invalid CB, pick a value between 0 and 16", "Can't",MessageBoxButtons.OK);
                                }
                        
                            } else
                            {
                                if (!Utilities.VerifyCPUKey(Utilities.HexStringToByteArray(cpukey)))
                                {
                                    result = MessageBox.Show("The entered CPU key is invalid. Please ensure the entered CPU key is valid.\n\nWould you like to try re-entering a key, or would you like a CPU Key to be generated for you?\n\n", "CPU Key Verification Failed", MessageBoxButtons.YesNo);

                                    if (result == DialogResult.Yes)
                                    {
                                        cpukey = Utilities.GenerateRandomCPUKeyString();
                                        txtCPUKey.Text = cpukey;

                                        if (cb == 0)
                                        {
                                            try
                                            {
                                                int updseq = Int32.Parse(txtUpdateSeq.Text);

                                                if (updseq > 80)
                                                {
                                                    MessageBox.Show("Over 80 on update LDV is not possible", "Update LDV too high", MessageBoxButtons.OK);
                                                }
                                                else if (updseq < 0)
                                                {
                                                    MessageBox.Show("Under 0 on update LDV is not possible", "Update LDV too low", MessageBoxButtons.OK);
                                                }
                                                else
                                                {
                                                    CalculateFuses();
                                                    GetFuseBitsToBlow();
                                                    CreatePatch();
                                                }
                                            }
                                            catch (FormatException ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }

                                        }
                                        else if (cb != 0)
                                        {

                                            result = MessageBox.Show("It seems that you have entered a non-zero CB, this means you are potientally blocking certain exploits depending on this value.\nIs this okay?", "NON-ZERO CB", MessageBoxButtons.YesNo);
                                            if (result == DialogResult.Yes && cb > 12)
                                            {
                                                result = MessageBox.Show("It seems that you have entered a CB greater than 12, this means your console will never boot.\nIs this okay? (HIGHLY SUGGESTED YOU CLICK NO)", "NON-ZERO CB", MessageBoxButtons.YesNo);
                                                if (result == DialogResult.Yes)
                                                {
                                                    try
                                                    {
                                                        updseq = Int32.Parse(txtUpdateSeq.Text);

                                                        if (updseq > 80)
                                                        {
                                                            MessageBox.Show("Over 80 on update LDV is not possible", "Update LDV too high", MessageBoxButtons.OK);
                                                        }
                                                        else if (updseq < 0)
                                                        {
                                                            MessageBox.Show("Under 0 on update LDV is not possible", "Update LDV too low", MessageBoxButtons.OK);
                                                        }
                                                        else
                                                        {
                                                            CalculateFuses();
                                                            GetFuseBitsToBlow();
                                                            CreatePatch();

                                                        }
                                                    }
                                                    catch (FormatException ex)
                                                    {
                                                        Console.WriteLine(ex.Message);
                                                    }


                                                }
                                                else
                                                {
                                                    //do nothing
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    int updseq = Int32.Parse(txtUpdateSeq.Text);

                                                    if (updseq > 80 || updseq < 0)
                                                    {
                                                        MessageBox.Show("Over 80 on update LDV is not possible", "Update LDV too high", MessageBoxButtons.OK);
                                                    }
                                                    else if (updseq < 0)
                                                    {
                                                        MessageBox.Show("Under 0 on update LDV is not possible", "Update LDV too low", MessageBoxButtons.OK);
                                                    }
                                                    else
                                                    {
                                                        CalculateFuses();
                                                        GetFuseBitsToBlow();
                                                        CreatePatch();

                                                    }
                                                }
                                                catch (FormatException ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            //do nothing
                                        }
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        //do nothing
                                    }
                                    else
                                    {

                                    }



                                }
                                else
                                {

                                }
                            }
                            

                        } 
                        
                    }
                }



                _FuseNibbles.Clear();
                fuseBitsToBlow.Clear();
                fuseLine01Bytes = null;
                File.Delete("Patch Debug.txt");

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (chkAuto.Checked)
            {
                switch (motherboard)
                {
                    #region Xenon
                    case "Xenon":
                        switch (hacktype)
                        {
                            case "JTAG":
                                patchesFileName = "patches_xenon.bin"; fusesPatchOffset = 0x538;
                                break;
                            case "Glitch":
                                patchesFileName = "patches_fat.bin"; fusesPatchOffset = 0x414;
                                break;
                            case "Glitch2":
                                patchesFileName = "patches_g2falcon.bin"; fusesPatchOffset = 0x424;
                                break;
                        }
                        break;
                    #endregion
                    #region Zephyr
                    case "Zephyr":
                        switch (hacktype)
                        {
                            case "JTAG":
                                patchesFileName = "patches_zephyr.bin"; fusesPatchOffset = 0x538;
                                break;
                            case "Glitch":
                                patchesFileName = "patches_fat.bin"; fusesPatchOffset = 0x414;
                                break;
                            case "Glitch2":
                                patchesFileName = "patches_g2falcon.bin"; fusesPatchOffset = 0x424;
                                break;
                        }
                        break;
                    #endregion
                    #region Falcon
                    case "Falcon":
                        switch (hacktype)
                        {
                            case "JTAG":
                                patchesFileName = "patches_falcon.bin"; fusesPatchOffset = 0x538;
                                break;
                            case "Glitch":
                                patchesFileName = "patches_fat.bin"; fusesPatchOffset = 0x414;
                                break;
                            case "Glitch2":
                                patchesFileName = "patches_g2falcon.bin"; fusesPatchOffset = 0x424;
                                break;
                        }
                        break;
                    #endregion
                    #region Jasper
                    case "Jasper":
                        switch (hacktype)
                        {
                            case "JTAG":
                                patchesFileName = "patches_jasper.bin"; fusesPatchOffset = 0x538;
                                break;
                            case "Glitch":
                                patchesFileName = "patches_fat.bin"; fusesPatchOffset = 0x414;
                                break;
                            case "Glitch2":
                                patchesFileName = "patches_g2jasper.bin"; fusesPatchOffset = 0x424;
                                break;
                            case "Glitch2m":
                                patchesFileName = "patches_g2mjasper.bin"; fusesPatchOffset = 0x5E0;
                                break;
                            case "DevGL":
                                patchesFileName = "patches_g2mjasper.bin"; fusesPatchOffset = 0x5E0;
                                break;
                        }
                        break;
                    #endregion
                    #region Trinity
                    case "Trinity":
                        switch (hacktype)
                        {

                            case "Glitch":
                                patchesFileName = "patches_trinity.bin"; fusesPatchOffset = 0x424;
                                break;
                            case "Glitch2":
                                patchesFileName = "patches_g2trinity.bin"; fusesPatchOffset = 0x424;
                                break;
                            case "Glitch2m":
                                patchesFileName = "patches_g2mtrinity.bin"; fusesPatchOffset = 0x5E0;
                                break;
                            case "DevGL":
                                patchesFileName = "patches_g2mtrinity.bin"; fusesPatchOffset = 0x5E0;
                                break;
                        }
                        break;
                    #endregion
                    #region Corona
                    case "Corona":
                        switch (hacktype)
                        {
                            case "Glitch2":
                                patchesFileName = "patches_g2corona.bin"; fusesPatchOffset = 0x434;
                                break;
                            case "Glitch2m":
                                patchesFileName = "patches_g2mcorona.bin"; fusesPatchOffset = 0x5C0;
                                break;
                            case "DevGL":
                                patchesFileName = "patches_g2mcorona.bin"; fusesPatchOffset = 0x5C0;
                                break;
                        }
                        break;
                    #endregion
                    #region CoronaWB
                    case "CoronaWB":
                        switch (hacktype)
                        {
                            case "Glitch2":
                                patchesFileName = "patches_g2corona_WB.bin"; fusesPatchOffset = 0x4AC;
                                break;
                            case "Glitch2m":
                                patchesFileName = "patches_g2mcorona_WB.bin"; fusesPatchOffset = 0x5F4;
                                break;
                            case "DevGL":
                                patchesFileName = "patches_g2mcorona_WB.bin"; fusesPatchOffset = 0x5F4;
                                break;
                        }
                        break;
                    #endregion
                    #region Corona4GB
                    case "Corona4GB":
                        switch (hacktype)
                        {
                            case "Glitch2":
                                patchesFileName = "patches_g2corona_WB4G.bin"; fusesPatchOffset = 0x1534;
                                break;
                            case "Glitch2m":
                                patchesFileName = "patches_g2mcorona_WB4G.bin"; fusesPatchOffset = 0x167C;
                                break;
                            case "DevGL":
                                patchesFileName = "patches_g2mcorona_WB4G.bin"; fusesPatchOffset = 0x167C;
                                break;
                        }
                        break;
                        #endregion
                }

                TryAutoApplyPatch();

                _FuseNibbles.Clear();
                fuseBitsToBlow.Clear();
                fuseLine01Bytes = null;
                File.Delete("out.bin");
                File.Delete("Patch Debug.txt");
            }
        }
        byte[] fuseLine01Bytes;
        private void CalculateFuses()
        {
            try
            {
                updseq = Int32.Parse(txtUpdateSeq.Text);
                #region Fuse Line 00
                for (int i = 0; i < Resource1.Fuse_Line_00.Length; i++)
                {
                    _FuseNibbles.Add((byte)((Resource1.Fuse_Line_00[i] & 0xF0) >> 4));
                    _FuseNibbles.Add((byte)(Resource1.Fuse_Line_00[i] & 0x0F));
                }
                #endregion
                #region Fuse Line 01
                fuseLine01Bytes = null;

                if (consoleType == Constants.PhatRetailConsoleType)
                {
                    fuseLine01Bytes = Resource1.Fuse_Line_01_Phat_Retail;
                }

                else if (consoleType == Constants.SlimRetailConsoleType)
                {
                    fuseLine01Bytes = Resource1.Fuse_Line_01_Slim_Retail;
                }

                else if (consoleType == Constants.DevKitConsoleType)
                {
                    fuseLine01Bytes = Resource1.Fuse_Line_01_DevKit;
                }

                else
                {
                    MessageBox.Show("Failed to calculate fuses due to an invalid console type. The invalid console type is: " + consoleType, "Invalid Console Type!");

                }

                for (int i = 0; i < fuseLine01Bytes.Length; i++)
                {
                    _FuseNibbles.Add((byte)((fuseLine01Bytes[i] & 0xF0) >> 4));
                    _FuseNibbles.Add((byte)(fuseLine01Bytes[i] & 0x0F));
                }
                #endregion
                #region Fuse Line 02
                for (int i = 0; i < 16; i++)
                {
                    if (i == cb - 1)
                    {
                        _FuseNibbles.Add(0xF);
                    }

                    else
                    {
                        _FuseNibbles.Add(0x0);
                    }
                }
                #endregion
                #region Fuse Line 03, 04, 05 and 06
                byte[] cpuKeyBytes = Utilities.HexStringToByteArray(cpukey);

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            int byteIndex = k + (8 * i);

                            _FuseNibbles.Add((byte)((cpuKeyBytes[byteIndex] & 0xF0) >> 4));
                            _FuseNibbles.Add((byte)(cpuKeyBytes[byteIndex] & 0x0F));
                        }
                    }
                }
                #endregion
                #region Fuse Line 07, 08, 09, 10 and 11
                for (int i = 0; i < 80; i++)
                {
                    if (i < updseq)
                    {
                        _FuseNibbles.Add(0x0F);
                    }

                    else
                    {
                        _FuseNibbles.Add(0x00);
                    }
                }

                #endregion
                if (_FuseNibbles.Count == Constants.FuseNibblesCount)
                {

                }
                else
                {
                    MessageBox.Show("Failed to calculate fuses as the final nibble count is invalid. This is a fatal bug - contact the developer for a fix.", "Invalid Fuse Nibble Count!");
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string GetFormattedFuseLinesString()
        {
            string retStr = "";

            for (int i = 0; i < 12; i++)
            {
                int amountToSkip = i * 16;

                if (i <= 9)
                {
                    retStr += "Fuse Line 0" + i + ": " + _FuseNibbles.Skip(amountToSkip).Take(16).ToList().NibblesToHex(false) + "\n";
                }

                else
                {
                    retStr += "Fuse Line " + i + ": " + _FuseNibbles.Skip(amountToSkip).Take(16).ToList().NibblesToHex(false) + "\n";
                }
            }

            retStr = retStr.Substring(0, retStr.Length - 1);    //Removes trailing new line
            return retStr;
        }
        List<int> fuseBitsToBlow = new List<int>();
        private void GetFuseBitsToBlow()
        {


            using (StreamWriter debugWriter = File.CreateText(@"Patch Debug.txt"))
            {
                int currentFuse = -1;
                foreach (byte fuseNibble in _FuseNibbles)
                {
                    currentFuse++;
                    if ((fuseNibble & (1 << 3)) != 0)
                    {
                        fuseBitsToBlow.Add(currentFuse);
                    }

                    currentFuse++;
                    if ((fuseNibble & (1 << 2)) != 0)
                    {
                        fuseBitsToBlow.Add(currentFuse);
                    }

                    currentFuse++;
                    if ((fuseNibble & (1 << 1)) != 0)
                    {
                        fuseBitsToBlow.Add(currentFuse);
                    }

                    currentFuse++;
                    if ((fuseNibble & (1 << 0)) != 0)
                    {
                        fuseBitsToBlow.Add(currentFuse);
                    }

                    string line = string.Format("{0}, {1}, {2}, {3} - 0x{4}",
                        Convert.ToInt32(((fuseNibble & (1 << 3)) != 0)),
                        Convert.ToInt32(((fuseNibble & (1 << 2)) != 0)),
                        Convert.ToInt32(((fuseNibble & (1 << 1)) != 0)),
                        Convert.ToInt32(((fuseNibble & (1 << 0)) != 0)),
                        fuseNibble.ToString("X2")
                        );

                    debugWriter.WriteLine(line);
                }

                int nibbleIndex = 0;
                for (int i = 0; i < Constants.FusesCount; i += 4)
                {
                    byte nibbleValue = 0x00;
                    if (fuseBitsToBlow.Contains(i))
                    {
                        nibbleValue |= 1 << 3;
                    }

                    if (fuseBitsToBlow.Contains(i + 1))
                    {
                        nibbleValue |= 1 << 2;
                    }

                    if (fuseBitsToBlow.Contains(i + 2))
                    {
                        nibbleValue |= 1 << 1;
                    }

                    if (fuseBitsToBlow.Contains(i + 3))
                    {
                        nibbleValue |= 1 << 0;
                    }

                    if (_FuseNibbles[nibbleIndex] != nibbleValue)
                    {
                        MessageBox.Show("Failed to verify fuse bits. This is a fatal bug - contact the developer for a fix.", "Fuse Bits Verification Failed!");

                    }

                    nibbleIndex++;
                }
            }


        }


        Int32 BaseAddress = 0x0000A684;
        Int32 PatchAddress = 0x0000C000;
        Int32 JumpAddress = 0x0000A788;
        Int32 BurnFuseAddress = 0x00009558;

        private void CreatePatch()
        {
            try
            {


                using (BinaryWriter writer = new BinaryWriter(File.Open("out.bin", FileMode.Create)))
                {
                    int burnFuseBranchInst = Convert.ToInt32((BurnFuseAddress) + 0x48000003);



                    #region Patch Code Short


                    if (fuseBitsToBlow != null)
                    {
                        writer.Write(BitConverter.GetBytes(((int)PatchAddress).AsBigEndian()));
                        writer.Write(BitConverter.GetBytes(((fuseBitsToBlow.Count * 2) + 2).AsBigEndian()));
                        writer.Write((0x38800000).AsBigEndian());   //Load r4 with 0, SleepTime for fuse blowing is 0

                        foreach (int fuseBit in fuseBitsToBlow)
                        {
                            int loadFuseInst = 0x38600000 + fuseBit;

                            writer.Write((loadFuseInst).AsBigEndian()); //Load our fuse bit
                            writer.Write(burnFuseBranchInst.AsBigEndian()); //Call HvpBurnFuse
                        }

                        int branchBackInst = Convert.ToInt32((BaseAddress) + 0x48000007);
                        writer.Write(branchBackInst.AsBigEndian());

                        #endregion

                        #region Patch Branch
                        writer.Write(BitConverter.GetBytes(((int)BaseAddress).AsBigEndian()));
                        writer.Write(BitConverter.GetBytes((0x00000003).AsBigEndian()));

                        int patchBranchInst = Convert.ToInt32((PatchAddress) + 0x48000003);
                        writer.Write(patchBranchInst.AsBigEndian());    //Let's branch to our custom burn code
                        writer.Write((0x7C7F1B78).AsBigEndian());   //Move the return value into r31

                        int jumpBranchInst = Convert.ToInt32((JumpAddress) + 0x48000003);
                        writer.Write(jumpBranchInst.AsBigEndian()); //Finally, jump to our finished burns function
                        #endregion

                        #region NOPs
                        //For safety, let's NOP out all the code in between our first patch and our jump back location
                        int nopCounts = Convert.ToInt32((JumpAddress - BaseAddress) / 4) - 3;

                        writer.Write(BitConverter.GetBytes(((int)BaseAddress + 12).AsBigEndian()));
                        writer.Write(BitConverter.GetBytes(nopCounts.AsBigEndian()));

                        for (long i = 0; i < nopCounts; i++)
                        {
                            writer.Write(BitConverter.GetBytes((0x60000000).AsBigEndian()));
                        }
                    }

                    else
                    {

                    }
                    #endregion
                }


            }

            catch (Exception ex)
            {

            }
        }
        private void cmConsoleType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        int fusesPatchOffset;
        string patchesFileName;
        private void TryAutoApplyPatch()
        {
            try
            {
                if (motherboard != "" && hacktype != "")
                {

                    string defaultPatchesFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Assets\Default Patches\" + "2.0.17559.0" + @"\" + patchesFileName;
                    if (File.Exists(defaultPatchesFilePath))
                    {
                        byte[] defaultPatches = File.ReadAllBytes(defaultPatchesFilePath);

                        string outputPath = patchesFileName;

                        using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
                        {
                            byte[] blowPatch = File.ReadAllBytes("out.bin");

                            writer.Write(defaultPatches, 0, (int)fusesPatchOffset);
                            writer.Write(blowPatch);
                            writer.BaseStream.Position = (int)fusesPatchOffset + blowPatch.Length;
                            writer.Write(defaultPatches, (int)fusesPatchOffset + 0x10, defaultPatches.Length - ((int)fusesPatchOffset + 0x10));
                        }


                    }
                }


            }

            catch
            {

            }
        }
    }
}
