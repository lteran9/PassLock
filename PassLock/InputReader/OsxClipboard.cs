using System.Runtime.InteropServices;

namespace PassLock.InputReader
{
   /// <summary>
   /// This class is used to set the clipboard text on Mac OS. We do not want to output plaintext passwords
   /// to the command line so instead we copy them to the clipboard.
   /// </summary>
   static class OsxClipboard
   {
      public static void SetText(string text)
      {
         var nsString = objc_getClass("NSString");
         IntPtr str = default;
         IntPtr dataType = default;

         try
         {
            str = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), text);
            dataType = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), NSPasteboardTypeString);

            var nsPasteboard = objc_getClass("NSPasteboard");
            var generalPasteboard = objc_msgSend(nsPasteboard, sel_registerName("generalPasteboard"));

            objc_msgSend(generalPasteboard, sel_registerName("clearContents"));
            objc_msgSend(generalPasteboard, sel_registerName("setString:forType:"), str, dataType);
         }
         finally
         {
            if (str != default)
            {
               objc_msgSend(str, sel_registerName("release"));
            }

            if (dataType != default)
            {
               objc_msgSend(dataType, sel_registerName("release"));
            }
         }
      }

      [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
      static extern IntPtr objc_getClass(string className);

      [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
      static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector);

      [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
      static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, string arg1);

      [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
      static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1, IntPtr arg2);

      [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
      static extern IntPtr sel_registerName(string selectorName);

      const string NSPasteboardTypeString = "public.utf8-plain-text";
   }
}