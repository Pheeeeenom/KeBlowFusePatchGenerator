### Firstly, I'm not your fucking mom. Use this responsibly.

### Enter the info as expected by the application. There isn't error checking for the drop down menu's. Don't be fucking stupid and try and click JTAG on image type and select Trinity motherboard.

### Once you get your patches file, replace it in 17559/bin of XeBuild. Rebuild your nand then run the .xex in the KeBlowFuses folder. You need the Media folder in the same directory as the xex.

### This *ONLY* works on 17559 kernel. Want to port it to another? Get all the proper addresses.

### And for the LOVE of GOD. If someone sends you a nand and tells you to flash it then run an xex, please fucking don't.

### This release is meant to help people that want to move CPU's from one console that was jtaggable to another.
### I.E moving a falcon cpu with a CB of 4 to a jasper motherboard that needs CB 5.

### Or if you wanna make a JTAG from a zero fuse cpu, that works too.

### Again, I'm not your fucking mom. Don't use this to brick people. I don't want to regret it. Thanks.

# KeBlowFusesPatchGenerator

### Features

- Allow the user to blow eFuses on the Xbox 360 Xenon CPU. 
- KeBlowFusePatchGenerator creates the main patch data for the user to put into the kernel areaa of the patches used in XeBuild.  Note: You need to remove fuse blowing protection in the patch file.
- KeBlowFuses creates the xex for the user to run.  For now, resuming threads causes the console to hang. To be fixed.

- **FOR THE BLIND: THERE IS NO GOING BACK ONCE YOU BLOW A FUSE. MAKE SURE WHAT YOU WANT TO BLOW IS CORRECT. THEN CHECK IT AGAIN. AND ONE LAST TIME FOR GOOD MEASURE.**

Left most bit is most significant. This is how the nibbles are interpreted. First nibble on Fuse set 00 being 0xC means 1100 in binary.

You will never want to burn CB if you're trying to create a JTAG console.


Fuse set example:

    | Fuseset 00:  | c0ffffffffffffff  Do not touch this after you have set it 
    | Fuseset 01:  | 0f0f0f0f0f0ff0f0  Retail Slim 
    | Fuseset 01:  | 0f0f0f0f0f0f0ff0  Retail Phat 
    | Fuseset 01:  | 0f0f0f0f0f0f0f0f  Devkit does not matter slim or phat
    | Fuseset 02:  | 0000000000000000  CB FUSE COUNT. LEAVE BLANK TO RUN ANY
    | Fuseset 02:  | 000F000000000000  CB SEQ 4. (Last JTAG CB for Falcon/Zephyr/Xenon)
    | Fuseset 02:  | 0000F00000000000  CB SEQ 5. (Last JTAG CB for jasper)
    | Fuseset 02:  | 000000F000000000  CB SEQ 7. (JTAG PATCHED. RGH1)
    | Fuseset 02:  | 00000000000F0000  CB SEQ 12. (RGH1 Patched, RGH1.2/2 only)
    | Fuseset 02:  | 000000000000FFFF  CB SEQ 13-16. (Don't fucking do this.) You'll need to glitch CB Fusecheck to boot. (RJTOP)
    | Fuseset 03:  | eed5b3ae123af5c0  CPU KEY. FIRST HALF
    | Fuseset 04:  | eed5b3ae123af5c0  CPU KEY. FIRST HALF
    | Fuseset 05:  | 1602d8ae9d2087e1  CPU KEY. SECOND HALF
    | Fuseset 06:  | 1602d8ae9d2087e1  CPU KEY. SECOND HALF
    | Fuseset 07:  | fffff00000000000  LDV Value. In this example it is 5. Count the F's
    | Fuseset 08:  | 0000000000000000  Continued LDV count to end
    | Fuseset 09:  | 0000000000000000
    | Fuseset 10:  | 0000000000000000
    | Fuseset 11:  | 0000000000000000
[![CC BY 4.0][cc-by-shield]][cc-by]

This work is licensed under a
[Creative Commons Attribution 4.0 International License][cc-by].

[![CC BY 4.0][cc-by-image]][cc-by]

[cc-by]: http://creativecommons.org/licenses/by/4.0/
[cc-by-image]: https://i.creativecommons.org/l/by/4.0/88x31.png
[cc-by-shield]: https://img.shields.io/badge/License-CC%20BY%204.0-lightgrey.svg
