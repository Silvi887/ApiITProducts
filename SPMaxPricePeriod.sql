use[ShopProductsDB]
GO
/****** Object:  StoredProcedure [dbo].[PensPar9aal1Update]    Script Date: 07/11/2025 16:13:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Silviya
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pFindMaxSalesCategory]
	-- Add the parameters for the stored procedure here
	@date1 as varchar(10),
	@date2 as varchar(10)
AS
BEGIN
	
	SET NOCOUNT ON;
select t.catname, sum(totalprice) as sumpriceperiod from (

select   s.SaleDate,s.totalprice ,  s.Quantity,c.[Name] as CatName, rank() over (partition by c.id order by  p.id) as r1

from  sales s inner join Products p on s.ProductId=p.Id
inner join Categoriesproducts cp on p.id= cp.Productid

inner join Categories c on cp.CategoryId=c.Id
where saledate >@date1 and saledate< @date2

) as t

group by  t.catname
order by sumpriceperiod desc
END
GO

--EXEC pFindMaxSalesCategory '2025-11-02','2025-11-08'
