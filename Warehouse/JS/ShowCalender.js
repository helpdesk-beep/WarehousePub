
			function ShowCalendar(SourceControl, DestinationControl)
			{
				popUpCalendar(SourceControl, DestinationControl,"dd/mm/yyyy");
				return false;
			}

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
		    
	        // SR#1 Start
	        // Convert strings to ints .
	        var day   = parseInt(dayStr,10);
	        var month = parseInt(monthStr,10);
	        var year  = parseInt(yearStr,10);
	        // SR#1 End
	        
	        if(args.IsValid)
	            args.IsValid = getDateStatus(day,month,year);
        	
        }
        
        
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



				// function for validation 
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
// function for validation 
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
 // check of
 // validation date field
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
			
			
			function compare()
			{
			 
			 a=(document.getElementById('AvlStackCap'));
			 b =( document.getElementById('txtwt'));
			//ram
			if(b.value<0)
		    {
			     alert("Negetive value not accetped");
			     b.value="";
		         b.focus();
			
			}
		    else
			    {
			        if ( Number(a.value) < Number(b.value))
			        {
			        alert("stack insufficent to Stack Pl check the value");
                    b.focus();
			        }
			 
			
			}
			//Added
			var checkOK = "0123456789/";
		    var checkStr = document.getElementById('txtwt').value;
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
				document.getElementById('txtwt').value=""
				document.getElementById('txtwt').focus();
				return (false);
			} 
			//		 
	        }
     
			
         // function for validation 
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
//Confirmation for Decimal values
  
//function NumericDecimalCheck(input, DecimalPlace)
//{
////  var num = input.value.replace(/\,/g,'');
////  if(!isNaN(num))
////  {
////    if(num.indexOf('.') > -1)
////    {
////       num = num.split('.');
////       num[0] = num[0].toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{15})/g,'$1.').split('').reverse().join('').replace(/^[\,]/,'');
////       if(num[1].length > DecimalPlace)
////       {
////         alert("1");
////         num[1] = num[1].substring(0,num[1].length-1);
////       }  
////       input.value = num[0]+'.'+num[1];        
////    }
////    else
////    {
////        alert("2");
////         //input.value = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{15})/g,'$1.').split('').reverse().join('').replace(/^[\,]/,''); 
////         input.value = num.toString().split('').join('').replace(/(?=\d*\.?)(\d{15})/g,'$1.').split('').join('').replace(/^[\,]/,''); 
////    }
////  }
////  else
////  { 
////	    if(isNaN(num))
////	    {
////	    input.value = input.value.replace(/[^\d\.]*/g,'');
////	    } 
////   }
  
//// }

 
 // validation check    
function AskForComment()
 {
      var bags = document.ctl00_ContentPlaceHolder1_txtnobags.value;
       var wt = document.ctl00_ContentPlaceHolder1_txtwt.value;
        

      
    if ( bags == '' || wt == '')
    {
     if ( bags == '')
          {
              alert("No. of Bags cannot be empty.");
				
				document.ctl00_ContentPlaceHolder1_txtnobags.focus();
				return (false);
		}
 if ( wt == '')
          {
              alert("Weight cannot be empty.");
				
				document.ctl00_ContentPlaceHolder1_txtwt.focus();
				return (false);
		}
	
		
		}		
  }
  
  //count dropdownlist box items
  function dropdowncount()
 {
      var _count = document.ctl00_ContentPlaceHolder1_txtnobags.itms.count;
      
       if ( _count == 0)
          {
              alert("No. of Bags cannot be empty.");
				
				document.ctl00_ContentPlaceHolder1_txtnobags.focus();
				return (false);
		}
 
  }
  
  //To Pop Up A Window
  function popMe(url)
  {
    var newWindow;
    newWindow=window.open(url,'MyWin','width=275,height=390,top=1,left=1');
//  if(window.focus())
//  {
//  
//  }
  }
  
  
  
  
  
  //validate date field
  
  
//percentage
function validate() {
  // Percent = document.frmPost.percent.value
  
  if ((ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".") == -1) && (ctl00_ContentPlaceHolder1_txtmoistcontent.value.length >= 3)) {
   alert("Percentage format is not correct");
    ctl00_ContentPlaceHolder1_txtmoistcontent.value = "";
    ctl00_ContentPlaceHolder1_txtmoistcontent.focus();
    return false;
  }
  if ((ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".")) == 4 || (ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".")) == 3 || (ctl00_ContentPlaceHolder1_txtmoistcontent.value.indexOf(".")) == 0) {
    alert("Invalid Percentage");
     ctl00_ContentPlaceHolder1_txtmoistcontent.value = "";
    ctl00_ContentPlaceHolder1_txtmoistcontent.focus();
    return false;
  }
  if (isNaN(ctl00_ContentPlaceHolder1_txtmoistcontent.value)==true) {
    alert("Enter Numeric values");
     ctl00_ContentPlaceHolder1_txtmoistcontent.value = "";
    ctl00_ContentPlaceHolder1_txtmoistcontent.focus();
    return false;
  }	
  return true;
}	
  
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
                     
                     

	

