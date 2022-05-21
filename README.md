# AZPro
Console application that checks if Logo program has intersection in O(nLog(n)) time

To comply time complexity, application employs sweep line algorithm, which uses AVL tree data structure. Both algorithm and tree was implemented within project.

Documentation (PL): [doc](https://github.com/KicunKrzysztof/AZPro/blob/main/AZ_dokumentacja.pdf).

## Examples
- intersection:
```
fd 10 lt 90 fd 5 lt 90 fd 5 lt 90 fd 10
--------------------------------------------------------
Program logo: "fd 10 lt 90 fd 5 lt 90 fd 5 lt 90 fd 10"
Zawiera przecięcie
```
- no intersection:
```
fd 10 lt 90 fd 30
--------------------------------------------------------
Program logo: "fd 10 lt 90 fd 30"
Nie zawiera przecięcia
```
