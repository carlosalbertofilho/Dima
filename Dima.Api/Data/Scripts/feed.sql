INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES (N'Alimentação', 'Despesas com alimentos e bebidas', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Saúde', 'Gastos com saúde e bem-estar', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Transporte', 'Custos com transporte e veículos', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Moradia', 'Despesas relacionadas à casa', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Educação', 'Gastos com educação e cursos', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Lazer', 'Despesas com atividades de lazer', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Roupas', 'Gastos com vestuário', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Investimentos', 'Investimentos e aplicações financeiras', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Impostos', 'Pagamentos de impostos e taxas', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Viagem', 'Despesas com viagens e turismo', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Presentes', 'Gastos com presentes e doações', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Beleza', 'Despesas com beleza e cuidados pessoais', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Pets', 'Gastos com animais de estimação', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Telefonia', 'Custos com telefonia e internet', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Seguros', 'Pagamentos de seguros diversos', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Saúde Mental', 'Despesas com psicologia e terapias', 'carlos@teste.com');
INSERT INTO [dbo].[Categorias] (Title, Description, UserId) VALUES ('Fitness', 'Gastos com academia e atividades físicas', 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Compra de supermercado', '2024-01-05', '2024-01-05', 2, -300.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Alimentação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Mensalidade Academia', '2024-01-10', '2024-01-10', 2, -89.99, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Fitness'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES (N'Passagem de ônibus', '2024-01-15', '2024-01-15', 2, -150.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Transporte'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Livros para curso', '2024-01-20', '2024-01-20', 2, -200.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Educação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Salário', '2024-01-25', '2024-01-25', 1, 5000.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Investimentos'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Consulta médica', '2024-01-26', '2024-01-26', 2, -250.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Saúde'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Jantar fora', '2024-01-27', '2024-01-27', 2, -120.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Lazer'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Ração para cachorro', '2024-01-28', '2024-01-28', 2, -75.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Pets'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Pagamento de seguro de vida', '2024-01-29', '2024-01-29', 2, -150.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Seguros'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Mensalidade Netflix', '2024-02-02', '2024-02-02', 2, -45.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Lazer'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Roupa nova', '2024-02-06', '2024-02-06', 2, -300.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Roupas'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Conserto do carro', '2024-02-11', '2024-02-11', 2, -800.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Transporte'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Corte de cabelo', '2024-02-15', '2024-02-15', 2, -50.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Beleza'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Compra de livros', '2024-02-18', '2024-02-18', 2, -120.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Educação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Reembolso de viagem', '2024-02-20', '2024-02-20', 1, 1500.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Viagem'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Aluguel de fevereiro', '2024-02-25', '2024-02-25', 2, -1500.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Moradia'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('IPVA', '2024-02-27', '2024-02-27', 2, -400.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Impostos'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Consulta veterinária', '2024-02-28', '2024-02-28', 2, -180.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Pets'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Jantar de aniversário', '2024-02-28', '2024-02-28', 2, -250.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Lazer'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Salário Mensal', '2024-03-01', '2024-03-01', 1, 5000.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Investimentos'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Conta de Luz', '2024-03-02', '2024-03-02', 2, -120.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Moradia'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Conta de Água', '2024-03-05', '2024-03-05', 2, -80.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Moradia'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Mensalidade Escolar', '2024-03-10', '2024-03-10', 2, -600.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Educação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Compra de Roupas', '2024-03-12', '2024-03-12', 2, -300.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Roupas'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Compra de Suplementos', '2024-03-15', '2024-03-15', 2, -200.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Saúde'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Restaurante com a Família', '2024-03-18', '2024-03-18', 2, -250.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Alimentação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Plano de Telefonia', '2024-03-20', '2024-03-20', 2, -150.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Telefonia'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Viagem de Fim de Semana', '2024-03-22', '2024-03-22', 2, -800.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Viagem'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Pagamento Seguro Carro', '2024-03-24', '2024-03-24', 2, -400.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Seguros'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, [Type], Amount, CategoryId, UserId)
VALUES ('Material de Arte', '2024-03-26', '2024-03-26', 2, -150.00, (SELECT TOP 1 Id FROM [dbo].[Categorias] WHERE Title='Lazer'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Receita de Freelance', '2024-05-02', '2024-05-02', 1, 2200.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Investimentos'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Conta de Internet', '2024-05-05', '2024-05-05', 2, -89.99, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Telefonia'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Despesa com Transporte', '2024-05-07', '2024-05-07', 2, -160.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Transporte'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Compra de Livros', '2024-05-09', '2024-05-09', 2, -120.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Educação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Salário', '2024-05-10', '2024-05-10', 1, 4000.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Investimentos'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Pagamento do Aluguel', '2024-05-12', '2024-05-12', 2, -1500.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Moradia'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Gastos com Jantar', '2024-05-15', '2024-05-15', 2, -200.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Alimentação'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Consulta Médica', '2024-05-18', '2024-05-18', 2, -300.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Saúde'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Ração para Pet', '2024-05-20', '2024-05-20', 2, -75.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Pets'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Presente de Aniversário', '2024-05-22', '2024-05-22', 2, -150.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Presentes'), 'carlos@teste.com');

INSERT INTO [dbo].[Transacoes] (Title, CreatedAt, PaidOrReceivedAt, Type, Amount, CategoryId, UserId)
VALUES ('Bonificação', '2024-05-24', '2024-05-24', 1, 1200.00, (SELECT Id FROM [dbo].[Categorias] WHERE Title='Investimentos'), 'carlos@teste.com');