USE [MutantDB]
GO

/****** Object:  StoredProcedure [dbo].[GetStats]    Script Date: 5/10/2020 4:47:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetStats]    
AS    
BEGIN 
WITH CTE 
AS(
    SELECT
  (SELECT COUNT(*) FROM Dnas t2
  where IsMutant = 1) AS count_mutant_dna,
  (SELECT COUNT(*) FROM Dnas t2
  where IsMutant = 0) AS count_human_dna,
count(*) as total
FROM Dnas
)
select count_mutant_dna, count_human_dna,
CAST(CAST(count_mutant_dna as float) / CAST(count_human_dna as float) as DECIMAL(36,2))as ratio
from cte

END
GO