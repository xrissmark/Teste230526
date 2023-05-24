--------------------------------------------------------------------------------------------
-- Listar todos os clientes do estado de SP que possuem mais de 60% das parcelas pagas

SELECT  C.*
FROM CLIENTES C
JOIN FINANCIAMENTOS F ON C.CPF = F.CPF
JOIN (
    SELECT	P.ID_FINANCIAMENTO, 
			COUNT(*) AS TOTAL_PARCELAS, 
			SUM(CASE WHEN P.DT_PAGAMENTO IS NOT NULL THEN 1 ELSE 0 END) AS PARCELAS_PAGAS
    FROM PARCELAS P
    GROUP BY P.ID_FINANCIAMENTO
) AS AUX ON F.ID = AUX.ID_FINANCIAMENTO
WHERE C.UF = 'SP' 
AND CAST((AUX.PARCELAS_PAGAS) AS FLOAT) / AUX.TOTAL_PARCELAS > 0.60;

--------------------------------------------------------------------------------------------
--Listar os primeiros quatro clientes que possuem alguma parcela com mais de cinco dia sem 
--atraso (Data Vencimento maior que data atual E data pagamento nula)

SELECT DISTINCT TOP 4 C.* FROM CLIENTES C
INNER JOIN FINANCIAMENTOS F ON F.CPF = C.CPF
INNER JOIN PARCELAS P ON P.ID_FINANCIAMENTO = F.ID

WHERE P.DT_VENCIMENTO > GETDATE() AND P.DT_PAGAMENTO IS NULL