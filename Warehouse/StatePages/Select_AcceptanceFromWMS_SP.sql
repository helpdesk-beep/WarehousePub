USE [Intergrated_MP_STORAGE]
GO

/****** Object:  StoredProcedure [dbo].[Get_Branch_Details]    Script Date: 22-12-2020 17:11:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--DF_Receipt_Id
--2301001048208D1
-- =============================================
CREATE PROCEDURE [dbo].[Get_Branch_Details]
@BranchID VARCHAR(20)
AS
BEGIN
	SELECT 
		R.Acpt_FCIRO_No,
		CONVERT(VARCHAR(10),Acpt_FCIRO_Date,103) Acpt_FCIRO_Date,
		Qty_Rvd_No_of_Bags Bags,
		Qty_Rvd_Weight QtyWeight 
	FROM tbl_Storage_Receipt_Details R 
		INNER JOIN (
			SELECT 
				DF_Receipt_Id,
				SUM(WLC_Bags) TotalBags,
				SUM(WLC_Qty) TotalQty
			FROM [dbo].[Receive_Proc_Kharif2020] 
			GROUP BY DF_Receipt_Id
		)K
			ON K.DF_Receipt_Id=R.Acpt_FCIRO_No
				AND K.TotalBags<>R.Qty_Rvd_No_of_Bags
				AND K.TotalQty<>R.Qty_Rvd_Weight 
	WHERE R.BranchId=@BranchID
END
GO

/****** Object:  StoredProcedure [dbo].[Get_Branch_Details_ReceiptID]    Script Date: 22-12-2020 17:11:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--
--2301001048208D1
-- =============================================
CREATE PROCEDURE [dbo].[Get_Branch_Details_ReceiptID]
@DF_Receipt_Id VARCHAR(70)
AS
BEGIN
	SELECT 
		R.Acpt_FCIRO_No,
		Acceptance_No,
		CONVERT(VARCHAR(10),Acceptance_Date,103) Acceptance_Date,
		Depositor_Form_No,
		TC_Number,
		Truck_Number,
		Qty_Rvd_No_of_Bags Bags,
		Qty_Rvd_Weight QtyWeight 
	FROM [dbo].[Receive_Proc_Kharif2020] K
		INNER JOIN tbl_Storage_Receipt_Details R
			ON K.DF_Receipt_Id=R.Acpt_FCIRO_No
	WHERE K.DF_Receipt_Id=@DF_Receipt_Id
	ORDER BY Acceptance_No
END
GO


