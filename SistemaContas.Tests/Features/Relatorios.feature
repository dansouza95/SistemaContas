Funcionalidade: Gerar relatórios do sistema

@SemLogin
Cenário: Gerar um extrato em PDF dos últimos 7 dias
Dado que estou na página "http://localhost:10941/Acesso/Login"
E preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
E clico na opcao "Entrar"
E clico no link "MenuRelatorios"
E clico no link "extrato7Dias"
E clico na opcao "btn_gerarExtrato"
E clico no link "Opcoes"
E clico no link "Sair"
Então devo ver o elemento "input"

@SemLogin
Cenário: Gerar um extrato em PDF dos últimos 15 dias
Dado que estou na página "http://localhost:10941/Acesso/Login"
E preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
E clico na opcao "Entrar"
E clico no link "MenuRelatorios"
E clico no link "extrato15Dias"
E clico na opcao "btn_gerarExtrato"
E clico no link "Opcoes"
E clico no link "Sair"
Então devo ver o elemento "input"

@SemLogin
Cenário: Gerar um extrato em PDF dos últimos 30 dias
Dado que estou na página "http://localhost:10941/Acesso/Login"
E preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
E clico na opcao "Entrar"
E clico no link "MenuRelatorios"
E clico no link "extrato30Dias"
E clico na opcao "btn_gerarExtrato"
E clico no link "Opcoes"
E clico no link "Sair"
Então devo ver o elemento "input"

@SemLogin
Cenário: Gerar um extrato em PDF completo
Dado que estou na página "http://localhost:10941/Acesso/Login"
E preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
E clico na opcao "Entrar"
E clico no link "MenuRelatorios"
E clico no link "extratoCompleto"
E clico na opcao "btn_gerarExtrato"
E clico no link "Opcoes"
E clico no link "Sair"
Então devo ver o elemento "input"