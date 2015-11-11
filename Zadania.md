# Komponenty

## Repozytorium
Repozytorium to implementacja wzorca o tej samej nazwie. Będziemy z niego korzystać aby komunikować się z bazą danych.
Podstawowe operacje do obsługi bazy danych będą wystawione w interfejsie repozytorium.
Dzięki temu wszystkie metody z niego korzystające będą w pełni testowalne - będą zależne od kontraktu, nie od implementacji.

## Serwis
Serwisy będą grupować funkcjonalność związaną z danym kontekstem, tak aby konsumenci mogli korzystać z logiki aplikacji w przystępny sposób.
Podobnie jak inne komponenty aplikacji będziemy się do nich odwoływać po interfejsie który implementują.

## Baza danych
Na potrzeby naszych zajęć użyjemy bazdy MS SQL Server LocalDB, która pozwala nam stworzyć rozbudowaną bazę danych na lokalnej maszynie.
Pozwoli to na niezależną pracę nad bazą danych bez konieczności łączenia się ze zdalnym serwerem.
