_<details><summary>Original README</summary>_
Zadanie testowe

Wykonaj poniższe założenia gry typu First Person Shooter:
- Na scenie powinno znajdować się 5 obiektów do zniszczenia (np beczka, hydrant, etc.) oraz
gracz,
- Wymienione wyżej obiekty mają na sobie informacje z jakiego rodzaju materiału są
zrobione oraz ich wytrzymałość,
- Materiał nie jest powiązany z klasą material wbudowana w Unity, lecz określa z czego
fizycznie wykonany jest obiekt,
- Gracz powinien się poruszać jak w klasycznym FPS,
- Gracz posiada 3 różne bronie – każda broń ma określone jaki rodzaj obiektu niszczy oraz jak
duże obrażenia robi – sposób zmiany broni jest dowolny,
- Gdy gracz celuje w obiekt powinien móc go zniszczyć, jeśli broń którą aktualnie dzierży ma
możliwość uszkodzenia danego rodzaju materiału,
- Zniszczony obiekt powinien mieć możliwość podpięcia pod siebie dowolnych funkcji, które
wywoływać się będą po jego zniszczeniu – np wywołanie particle effects, lub otworzenie
drzwi, etc. Obiekt powinien móc przyjąć dowolne zachowanie bez konieczności dopisywania
kodu do jego klasy,
- Wszystkie elementy graficzne typu animacja, particle effects, etc. wpływaja korzystnie na
zadanie testowe,
- Wszystkie elementy zadania testowego powinny być traktowane jako część większego
rozszerzalnego systemu,
- Zadanie testowe powinno zostać wysłane jako pełny projekt wykonany w Unity3D w wersji
2021.3.5f1, prosimy również umieścić w projekcie informacje na temat sterowania.
  
Wykonane zadanie prosimy wysłać na adres e-mail: praca@beastgamesofficial.com

Beast Games S.A.
</details>

</br>

- [x] Unity 2021.3.5f1
- [x] klasyczny FPS
  - [x] testowa scena
    > zaimportowana z szablonu `First Person`, materiały podmienione na `Default-Material`
  - [x] kontrola   
    > `PlayerController` napisany od podstaw, wykorzystuje `CharacterController` i `InputSystem`
    > input jest zczytywany tylko i wyłącznie na eventach (zamiast pollingu co klatkę w `Update`)
    - [x] postać _(WSAD)_
      > poruszanie się z początkowym przyspieszeniem, grawitacja
    - [x] kamera _(myszka)_
      > lewo-prawo obraca cała postać, obrót góra-dól obraca tylko kamerami i bronią
    - [x] strzał _(lewy przycisk myszki)_
    - [x] zmiana broni _(prawy przycisk myszki)_
- [x] 3 bronie z atrubytami:
  > modele z asset store: [First Person Lover - Weapons Pack](https://assetstore.unity.com/packages/3d/props/guns/first-person-lover-weapons-pack-39976)
  > utworzone bronie: Kusza, Shotgun, Granatnik
  - [x] zdawane obrażenia
    > bazowe obrażenia, modyfikowane przez poniższy system
  - [x] materiały, na które działa
    > system modyfikatorów obrażeń przeciwko poszczególnym materiałom, edytowalny w inspektorze. Przykład:  
    > jeśli trafisz w `drewno`, zadaj `100%` obrażeń  
    > jeśli trafisz w `kamień`, zadaj `25%` obrażeń  
    > w przeciwnym wypadku zadaj `0%` obrażeń
- [x] 5 zniszczalnych obiektów z atrybutami:
    > modele z asset store: [Low Poly Brick House](https://assetstore.unity.com/packages/3d/props/exterior/low-poly-brick-houses-131899), [Low Poly Rock Pack](https://assetstore.unity.com/packages/3d/environments/low-poly-rock-pack-57874), [Low Poly Tree Pack](https://assetstore.unity.com/packages/3d/vegetation/trees/low-poly-tree-pack-57866), [Stylized Crystal](https://assetstore.unity.com/packages/3d/props/stylized-crystal-77275)
    > utworzone zniszczalne obiekty: Krzak, Drzewo, Kamienie, Lampa, Kryształ
  - [x] wytrzymałość
  - [x] materiał
    > utworzone materiały: Roślinny, Drewniany, Kamienny, Metalowy, Kryształowy
- [x] event na zniszczenie obiektu
    > event na zmiane hp obiektu, z parametrów można wywnioskować czy obiekt został zniszczony
- [x] rozszerzalne rozwiązania
    > główne komponenty składane z paru mniejszych (kompozycja)
    > sporo `UnityEvent`ów używalnych z inspektora
- [ ] opcjonalnie: szata graficzna (animacje, particle)
