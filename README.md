![image](https://github.com/Vheos/Interview.BeastGames/assets/9155825/69906934-c420-4c2d-9f8c-6df7b34e1f24)

- [x] Na scenie powinno znajdować się 5 obiektów do zniszczenia (np beczka, hydrant, etc.) oraz gacz,
  > obiekty do zniszczenia mają komponent 'Destructible' i są to:
  > Krzak, Drzewo, Skała, Lampa, Kryształ
- [x] Wymienione wyżej obiekty mają na sobie informacje z jakiego rodzaju materiału są zrobione oraz ich wytrzymałość. Materiał nie jest powiązany z klasą material wbudowana w Unity, lecz określa z czego fizycznie wykonany jest obiekt,
  > wytrzymałość to standardowo 'Health' / HP
  > materiał wykonania określiłem jako 'ArmorType' / Armor
  > obiekty posiadają komponent 'Destructible' i są to:
  > - krzak - 30 HP / Plant armor
  > - drzewo - 35 HP / Wood armor
  > - skała - 40 HP / Stone armor
  > - latarnia - 45 HP / Metal armor
  > - kryształ - 50 HP / Crystal armor
- [x] Gracz powinien się poruszać jak w klasycznym FPS,
  > klasyczny WSAD + Spacja
- [x] Gracz posiada 3 różne bronie – każda broń ma określone jaki rodzaj obiektu niszczy oraz jak duże obrażenia robi – sposób zmiany broni jest dowolny,
  > - kusza - natychmiastowy hitscan (raycast)
  > - shotgun - 6 rozproszonych pocisków (rigidbody, continuous collision)
  > - granatnik - strzał granatem (rigidbody), po 2 sekundach eksplozja (spherecast)
- [x] Gdy gracz celuje w obiekt powinien móc go zniszczyć, jeśli broń którą aktualnie dzierży ma możliwość uszkodzenia danego rodzaju materiału,
  > zamiast binarnego "może / nie może zniszczyć" stworzyłem system modyfikatorów obrażeń, gdzie każda broń posiada:
  > - bazowe obrażenia
  > - listę mnożników obrażeń zależnie od materiału celu
  > - domyślny mnożnik jeśli nie sprecyzowano danego materiału

  > Przykład:
  > - kusza zadaje 4 obrażenia
  > - roślinnemu materiałowi zadaje pełne obrażenia
  > - drewnianemu - tylko 25% bazowych obrażeń, czyli 1
  > - każdemu innemu - zaledwie 5% obrażeń, więc 0.2
- [x] Zniszczony obiekt powinien mieć możliwość podpięcia pod siebie dowolnych funkcji, które wywoływać się będą po jego zniszczeniu – np wywołanie particle effects, lub otworzenie drzwi, etc. Obiekt powinien móc przyjąć dowolne zachowanie bez konieczności dopisywania kodu do jego klasy,
  > - zastosowałem ładnie opakowane UnityEvents. Z dostępem do Odina można by się pokusić o jeszcze bardziej modularny system
- [x] Wszystkie elementy graficzne typu animacja, particle effects, etc. wpływaja korzystnie na zadanie testowe,
  > - testowa scena zaimportowana z szablonu Unity 'First Person', materiały podmienione na 'Default-Material'
  > - zniszczalne obiekty posiadają animacje otrzymywania obrażeń oraz zniszczenia
  > - każdy rodzaj materiału obiektu wywołuje inny particle effect przy trafieniu
  > (np. strzał w drzewo wyrzuca wióry, a w metal - iskry)
  > - pociski zostawiają za sobą smugi (TrailRenderer)

  > Źródła assetów:
  > - broni: [First Person Lover - Weapons Pack](https://assetstore.unity.com/packages/3d/props/guns/first-person-lover-weapons-pack-39976)
  > - zniszczalnych obiektów: [Low Poly Brick House](https://assetstore.unity.com/packages/3d/props/exterior/low-poly-brick-houses-131899), [Low Poly Rock Pack](https://assetstore.unity.com/packages/3d/environments/low-poly-rock-pack-57874), [Low Poly Tree Pack](https://assetstore.unity.com/packages/3d/vegetation/trees/low-poly-tree-pack-57866), [Stylized Crystal](https://assetstore.unity.com/packages/3d/props/stylized-crystal-77275)
  > - particle effects: [Unity Particle Pack](https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325)
  > - animacje przy użyciu [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- [x] Wszystkie elementy zadania testowego powinny być traktowane jako część większego rozszerzalnego systemu,
  > - zero dziedziczenia klas, cała rozszerzalność dzięki kompozycji
  > - używanie prefab variantów gdzie tylko można
  > - dzielenie obiektów na niewielkie, wyspecjalizowane klasy
  > - sporo UnityEventów edytowalnych z inspektora
- [x] Zadanie testowe powinno zostać wysłane jako pełny projekt wykonany w Unity3D w wersji
2021.3.5f1, prosimy również umieścić w projekcie informacje na temat sterowania.
  > podczas rozgrywki sterowanie jest wypisane w UI, jak na obrazku

</br>

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
