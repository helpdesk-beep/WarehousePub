// JScript File


 ////function start
   var message="Sorry,Right Click is Disabled For Security Purpose."
 function click(e) 
              {
                 if (document.all) 
                 {
                   if (event.button == 2) 
                   {
                     alert(message);
                     return false;
                   }
                  }
                 if (document.layers) {
                   if (e.which == 3) {
                      alert(message);
                       return false;
                    }
                 }
              }
    if (document.layers) {
    document.captureEvents(Event.MOUSEDOWN);
     }
    document.onmousedown=click;
    
   ////function end
   
  ////function start
    function validateDate(oSrc, args)
        {	            
	        var dateString=args.Value;
	        var delimeter = "/";
	        var dayStr;
	        var monthStr;
	        var yearStr;
	        var	strDateArray;
        	
	        if (dateString.indexOf(delimeter) != -1)
	        {
		        strDateArray = dateString.split(delimeter);
		    
		        if (strDateArray.length != 3)
			        args.IsValid = false;
		        else
		        {
			        dayStr		= strDateArray[0];
			        monthStr	= strDateArray[1];
			        yearStr		= strDateArray[2];        			   
		        }
	        }
	        else
		        args.IsValid = false;
		        
		    
	        if(dayStr.length == 0 || monthStr.length == 0 || yearStr.length != 4 )
		        args.IsValid = false;
	        if(isNaN(dayStr))
		        args.IsValid = false;
	        if(isNaN(monthStr))
		        args.IsValid = false;
	        if(isNaN(yearStr))
		        args.IsValid = false;
		    
	       
	        var day   = parseInt(dayStr,10);
	        var month = parseInt(monthStr,10);
	        var year  = parseInt(yearStr,10);
	      
	        
	        if(args.IsValid)
	            args.IsValid = getDateStatus(day,month,year);
        	
        }
  ////function end
  
  
  ////function start
  
   function validateDatenew(dtControl) 
{
    var input = document.getElementById(dtControl)
    var validformat=/^\d{1,2}\/\d{1,2}\/\d{4}$/ //Basic check for format validity
    var returnval=false
    if (!validformat.test(input.value))
    alert('Invalid Date Format. Please correct.')
    else{ //Detailed check for valid date ranges
    var dayfield=input.value.split("/")[0]
    var monthfield=input.value.split("/")[1]
    var yearfield=input.value.split("/")[2]
    
    var dayobj = new Date(yearfield, monthfield-1, dayfield)
    if ((dayobj.getMonth()+1!=monthfield)||(dayobj.getDate()!=dayfield)||(dayobj.getFullYear()!=yearfield))
    alert('Invalid Day, Month, or Year range detected. Please correct.')
    else
    {
        returnval=true
    }
    }
    if (returnval==false) 
     input.value =""
    return returnval
} 
 ////function end
 
 
 ////function start
 function Spc_validatornumeric(abc)
   {
		var checkOK = "0123456789.";
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

 
 
 ////function start
 function Spc_validator(abc)
   {
		var checkOK = "0123456789";
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
 ////function end
 
 
 ////function start
 function Spc_validatordate(abc)
   {
		var checkOK = "0123456789/";
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
 ////function end
 
 ////function start
 
   function Spc_character(abc)
             {
		var checkOK = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,()_&\ ";;
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
 ////function end 
  ////function start
 
 function NumericDecimalCheck(input, DecimalPlace)
                        {
                      var num = input.value.replace(/\,/g,'');
                      if(!isNaN(num))
                      {
                        if(num.indexOf('.') > -1)
                        {
                           num = num.split('.');
                           num[0] = num[0].toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{15})/g,'$1.').split('').reverse().join('').replace(/^[\,]/,'');
                           if(num[1].length > DecimalPlace)
                           {
                             num[1] = num[1].substring(0,num[1].length-1);
                           }  input.value = num[0]+'.'+num[1];        
                         } else{ input.value = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{15})/g,'$1.').split('').reverse().join('').replace(/^[\,]/,'') };
                       }
                       else{ 
	                      if(isNaN(num)){
	                         input.value = input.value.replace(/[^\d\.]*/g,'');
	                      } 
                       }
                     }	
                     
////function end  


////function start

	function Spc_characteralpha(abc)
   {
		var checkOK = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,\ ";;
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
	////function end	
	////function start		
			 function Spc_validatorInt(abc)
           {
                var checkOK = "0123456789";
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
	                    alert("Only Integer values Allowed");
	                    abc.value=""
	                    abc.focus();
	                    return (false);
                    }  
           }
           
           
       ////function end	
	////function start	    
            function validateDatequality(oSrc, args)
        {	            
	        var dateString=args.Value;
	        var delimeter = "/";
	        var dayStr;
	        var monthStr;
	        var yearStr;
	        var	strDateArray;
        	
	        if (dateString.indexOf(delimeter) != -1)
	        {
		        strDateArray = dateString.split(delimeter);
		    
		        if (strDateArray.length != 3)
			        args.IsValid = false;
		        else
		        {
			        dayStr		= strDateArray[0];
			        monthStr	= strDateArray[1];
			        yearStr		= strDateArray[2];        			   
		        }
	        }
	        else
		        args.IsValid = false;
		        
		    
	        if(dayStr.length == 0 || monthStr.length == 0 || yearStr.length != 4 )
		        args.IsValid = false;
	        if(isNaN(dayStr))
		        args.IsValid = false;
	        if(isNaN(monthStr))
		        args.IsValid = false;
	        if(isNaN(yearStr))
		        args.IsValid = false;
		    
	        // SR#1 Start
	        // Convert strings to ints .
	        var day   = parseInt(dayStr,10);
	        var month = parseInt(monthStr,10);
	        var year  = parseInt(yearStr,10);
	        // SR#1 End
	        
	        if(args.IsValid)
	            args.IsValid = getDateStatus(day,month,year);
        	
        }
        ////function end	
	////function start	
	
	 function textCounter(field, maxlimit) {
if (field.value.length > maxlimit) // if too long...trim it!
{
	alert ("Enter upto 50 characters");
	field.value = field.value.substring(0, maxlimit);
}
else // otherwise, update 'characters left' counter
{
	//countfield.value = maxlimit - field.value.length;
}
}

//function to check the decimal numbers


function checkDecimals(fieldName, fieldValue) 
{
//onblur= "checkDecimals(txtcatD, txtcatD.value)"
decallowed = 2;  // how many decimals are allowed?

if (isNaN(fieldValue) || fieldValue == "") 
{
    alert("Oops!  That does not appear to be a valid number.  Please try again.");
    fieldName.select();
    fieldName.focus();
}
else 
{
    if (fieldValue.indexOf('.') == -1) 
    {
        fieldValue += ".";
        dectext = fieldValue.substring(fieldValue.indexOf('.')+1, fieldValue.length);

        if (dectext.length > decallowed)
        {
            alert ("Oops!  Please enter a number with up to " + decallowed + " decimal places.  Please try again.");
            fieldName.select();
            fieldName.focus();
        }
    }
}
} 

 ////function end	
	////function start	
	
	function textCounter(field, maxlimit) {
if (field.value.length > maxlimit) // if too long...trim it!
{
	alert ("Enter upto 50 characters");
	field.value = field.value.substring(0, maxlimit);
}
else // otherwise, update 'characters left' counter
{
	//countfield.value = maxlimit - field.value.length;
}
}

//function to check the decimal numbers


function checkDecimals(fieldName, fieldValue) 
{
//onblur= "checkDecimals(txtcatD, txtcatD.value)"
decallowed = 2;  // how many decimals are allowed?

if (isNaN(fieldValue) || fieldValue == "") 
{
    alert("Oops!  That does not appear to be a valid number.  Please try again.");
    fieldName.select();
    fieldName.focus();
}
else 
{
    if (fieldValue.indexOf('.') == -1) 
    {
        fieldValue += ".";
        dectext = fieldValue.substring(fieldValue.indexOf('.')+1, fieldValue.length);

        if (dectext.length > decallowed)
        {
            alert ("Oops!  Please enter a number with up to " + decallowed + " decimal places.  Please try again.");
            fieldName.select();
            fieldName.focus();
        }
    }
}
}
////function end	
	////function start	
	
	
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
        
       ////function end	
	////function start	 
        function CheckOnlyNumeric(e, tx) {
            var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;           
            if ((AsciiCode < 46 && AsciiCode != 8 && AsciiCode != 9 ) || (AsciiCode > 57)  ) {
                alert('Please enter only numbers !');
                return false;
            }           
            
        }
         ////function end	