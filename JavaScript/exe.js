function   Run(strPath)   {   
    exe.value=strPath;   
    try   {   
    var   objShell   =   new   ActiveXObject("wscript.shell");   
    objShell.Run(strPath);   
    objShell   =   null;   
    }   
    catch   (e){alert('file not found: '+strPath)   
      
    }   
    }