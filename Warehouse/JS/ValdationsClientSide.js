// JScript File
function Spc_characteralpha(abc)
   {
		var checkOK = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,\ ";;
		var checkStr = abc.value;
		var allValid = true;
		var allChr = "";
		for (i = 0;  i < checkStr.length;  i++)
		{
			ch = checkStr.charAt(i);
			for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
			break;
			if (j == checkOK.length)
			{
				allValid = false;
				break;
			}
			if (ch != ",")
				allChr += ch;
		}
			if (!allValid)
			{
				alert("This Character is Not Allowed");
				abc.value=""
				abc.focus();
				return (false);
			}  
	}  
 function CheckIsNumeric(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;           
            if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9 ) || (AsciiCode > 57) || (AsciiCode == 47)) {
                alert('Please enter only numbers !');
                tx.value="";
                return false;
            }
            var num = tx.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if(len > 5 && indx == -1)
            {
                if(AsciiCode==46)
                {
                
                }
                 else
                 {
                    alert('Only 6 digits are allowed before decimal');
                    return false;
                 }
                
            }
            
            if (indx != -1) {
                if ((AsciiCode == 46)) {
                tx.value="";
                    alert('Point must be apear only one time !');
                    return false;
                }
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);
                if (count > 5 && AsciiCode != 8 && AsciiCode != 9) {
                    alert("Only 5 digits allowed after decimal !");
                    return false;
                }
            }
        }
        
        
        function CheckOnlyNumeric(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;           
            if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9 ) || (AsciiCode > 57)  ) {
                alert('Please enter only numbers !');
                return false;
            }           
            
        }
        
    