## :pencil: Project 1 Description BG :pencil:

### Проект 1 (branch Project_1_Solution) - Цели числа и техните прости фактори ###

Вземаме предвид, че всяко не-простo число '𝑛' можем да представим като произведение на по-малки от него прости числа, както следва: 𝑛 = 𝑝1𝑝2 … 𝑝𝑘. Нека построим граф, който показва числата до 1000 като върхове, и оцветява простите числа в различен цвят. Ребрата свързват дадено не-просто число със всичките му прости фактори (които, разбира се, трябва да бъдат открити/пресметнати при построяването на графа). Имплементираната програма трябва да поддържа функционалността за експортиране на 
генерираните графи в GEXF формат, който описва типа граф, множеството от върховете и 
техните обозначения, както и ребрата (ID на върха-начало и ID на върха-край).

Изискванията са описани в Project 1 - Graph Theory 2022.pdf.\

## :pencil: Project 1 Description EN :pencil:

### Project 1 (branch Project_1_Solution) - Integers and their prime factors

We take into account that any non-simple number '𝑛' can be represented as a product of prime numbers smaller than it, as follows: 𝑛 = 𝑝1𝑝2 ... 𝑝𝑘. Let's construct a graph that shows numbers up to 1000 as vertices, and colors the prime numbers a different color. The edges connect a given non-simple number to all its prime factors (which, of course, must be found/calculated when constructing the graph). The implemented program must support the functionality to export the 
generated graphs in GEXF format, which describes the graph type, the set of vertices and the 
their labels, as well as the edges (vertex-start ID and vertex-end ID).

### Results:

![Exported Graph Image](https://github.com/gesho99/MDGP/blob/main/graph.png)

## :pencil: Project 2 Description BG :pencil:
### Проект 2 (branch main) - Семестриален курсов проект част 2 на мястото на писмен изпит в контекста на дистанционно обучение ##

Този практически насочен проект цели да затвърди уменията на студентите да работят с графи и да имплементират програми, чиято основа е обработката на графи и релевантните към тях структури данни. Въпреки, че изкуствени данни генерирани на случаен принцип биха могли да бъдат използвани за целта на проекта, работата с реални данни би била по-интересна. Накратко, при дадена матрица на корелациите, програмата трябва да генерира граф с тегло, ребрата с малки тегла да бъдат премахнати, покриващо дърво да бъде намерено, и накрая, резултатите да бъдат върнати в подходящ xml формат. Студентите следва да демонстрират работещата програма, и да обяснят своя сорс код и получените резултати. По-долу не само е описан прокета, но има и някои препоръки как да бъде изпълнен добре.

Описание на входните данни: 

Налични са два независими един от друг входни файла във csv формат – първият е матрица на корелациите на логаритмичната възвръщаемост (log-returns) на 25 акции на компании с голяма пазарна капитализация, а вторият съответства на матрицата на корелациите на волатилностите на същите 25 акции, пресметнати за периода 01/01/2020 до 12/12/2020. Всяка от тези квадратни матрици има по 25 реда и колони и може да бъде разглеждана като матрица на съседство на напълно свързан граф с тегло. Нека също обърнем внимание, че едиснтвения фокус на този проект е Теорията на графите и тяхната практическа имплементация. Как точно тези матрици са пресметнати ще бъде обяснено накратко по време на лекциите и упражненията (т.е. какво е корелация, log-returns, волатилност, дискретни времеви серии), и оценките няма да зависят от познанието по тази материя. При все това, окуражаваме всички студенти, които проявяват интерес към темата да направят свое собствено проучване. И двета матрици са симетрични с по 25 реда и колони. Техните елементи са реални числа между -1 и 1, а всички коефициенти по диагоналите са равни на 1.0 (свойство на коефициентите на корелация). И двете матрици са достъпни в CSV (comma separated value) формат със сепаратор “,”. 

Изисквания за имплементацията:

1. Език за програмиране: Python, Java или C# са препоръчителни; все пак, студентите имат възможност да изберат друг език по тяхно желание, напр. C++, R, Rust и т.н. 
2. Матриците дадени като входни данни трябва да бъдат прочетени с помощта на стандартни file reading/writing техники и библиотеки (функционалност поддържана от повечето езици). Студентите имат свободата да изберат какви типове колекции и структури данни да ползват за временно съхранение на данните, като напр. Maps/Dictionaries, Lists of Lists, two-dimensional arrays, и т.н. 
3. Студентите могат да изберат как да имплементират структураата за техните графи, включваща върховете, ребрата, getters, setters, релевантни алгоритми, или също да правят пресмятанията директно върху двумерни масиви. Първата опция би била по-лесна за студенти с познания по компютърни науки и програмиране, съответно е препоръчителна. Имплементираната структура на граф трябва да бъде попълнена с данните от CSV файловете. Бележка: интерпретирайте графите като ненасочени, следователно, вместо да построявате едно ребро с начало връх i и край връх j и още едно ребро с обратна посока, просто построете едно ненасочено ребро между i and j. 
4. След като напълно свързаните графи са заредени, ребрата с теглови коефициенти съответстващи на слаба корелация трябва да бъдат преманати. За всеки връх, да се запазят само ребрата съответстващи на 3-те теглови коефициента с най-голяма абсолютна стойност. 

Бележки: 
* Интересуваме се от силните корелации (с коефициенти между 0 и 1) и анти-корелации (още, отрицателни корелации, с коефициенти между 0 и -1). Затова, пресметнете абсолютните стойности на тегловите коефициенти, тъй като стойности близки до 0 означават слаба статистическа взаимовръзка на върховете. 
** за веки връх целим да запазим свързаните с него ребра с най-голяма абсолютна стойност на теглото: така, итерирайки върху множеството от върховете, можем да съхраним всички ребра отговарящи на изискването във временна „white-list“ структура (напр. HashMap/Dictionary съдържащ информация за върховете начало и край на всяко избрано ребро). След това, може да се направи итерация върху множеството от всички ребра в графа, и да се изтрият тези, които не са включени в описаната горе „white-list“ структура. Забележете, че в така получения подграф на началния граф, някои върхове могат да имат повече от 3 съседа. 
5. Получените два подграфа на двата начални графа трябва да бъдат записани в xml файл (GEXF format, четим от софтурера Gephi) т.е. трябва да се генерират два текстови файла за всеки от под-графите, съответстващи на GEXF XSD схемата (спецификация: https://gephi.org/gexf/format/schema.html , по-долу има пример). 
6. Горната програма да бъде разширена с функционалността за пресмятане на максимални покриващи дървета (maximum spanning trees) извлечени от описаните горе подграфи.

Бележки: 
* Обърнете внимание, че намирането на максимално покриващо дърво е аналогично на откриването на минимално покриащо дърво, единствено трябва да се направи инверсия на тегловите коефициенти (или да се инвертира компаратора, в случай че ползвате такъв). Алгоритъма на Kruskal би бил добър избор в случая. 
** За да установите дали операцията добавяне на ребро образува цикъл, припомнете си, че алгоритмите Deep First Search / Breadth First Search могат да бъдат от полза. 
7. Подобно на заданието за т. 5, получените две покриващи дървета да бъдат запазени в xml файл (GEXF format) т.е. да се генерира текстови файл за всяко дърво, в съответствие с GEXF XSD xml схемата. 
8. Накрая, отворете получените 2 + 2 XML файла с помощта на софтуера Gephi и опитайте да подобрите ръчно визуализацията на получените графи. От менюто на Gephi, изберете да експортирате графите в PNG формат.

## :pencil: Project 2 Description EN :pencil:

### Project 2 (branch main) - Semester course project part 2 in place of a written examination in a distance learning context

This hands-on project aims to consolidate students' skills in working with graphs and implementing programs based on graph processing and the relevant data structures. Although artificial data generated randomly could be used for the purpose of the project, working with real data would be more interesting. In short, given a matrix of correlations, the program should generate a weighted graph, edges with small weights should be removed, a covering tree should be found, and finally, the results should be returned in an appropriate xml format. Students should demonstrate the program running, and explain their source code and the results obtained. The following not only describes the sample code, but also has some recommendations on how to execute it well.

Input description: 

There are two input files in csv format that are independent of each other - the first is a matrix of correlations of logarithmic returns (log-returns) of 25 stocks of large market capitalization companies, and the second corresponds to a matrix of correlations of volatilities of the same 25 stocks computed over the period 01/01/2020 to 12/12/2020. Each of these square matrices has 25 rows and columns and can be viewed as a neighborhood matrix of a fully connected weight graph. Let us also note that the singular focus of this project is Graph Theory and its practical implementation. Exactly how these matrices are computed will be explained briefly during the lectures and exercises (i.e., what is correlation, log-returns, volatility, discrete time series), and grades will not depend on knowledge of this subject matter. However, we encourage all students interested in the topic to do their own research. Both matrices are symmetric with 25 rows and columns each. Their elements are real numbers between -1 and 1, and all coefficients on the diagonals are equal to 1.0 (a property of correlation coefficients). Both matrices are available in CSV (comma separated value) format with separator ",". 

Implementation requirements:

1. Programming language: Python, Java, or C# are recommended; however, students have the option to choose another language of their choice, e.g., C++, R, Rust, etc. 
2. Matrices given as input must be read using standard file reading/writing techniques and libraries (functionality supported by most languages). Students have the freedom to choose what types of collections and data structures to use for temporary data storage, e.g. Maps/Dictionaries, Lists of Lists, two-dimensional arrays, etc. 
3. Students can choose how to implement the structure for their graphs, including vertices, edges, getters, setters, relevance algorithms, or also do the computations directly on two-dimensional arrays. The first option would be easier for students with a background in computer science and programming, and is recommended accordingly. The implemented graph structure should be populated with the data from the CSV files. Note: interpret the graphs as undirected, hence instead of constructing an edge starting at vertex i and ending at vertex j and another edge with the opposite direction, simply construct an undirected edge between i and j. 
4. Once the fully connected graphs are loaded, the edges with weights corresponding to weak correlation should be rescaled. For each vertex, keep only the edges corresponding to the 3 weight coefficients with the largest absolute value.

Notes: 
* We are interested in strong correlations (with coefficients between 0 and 1) and anti-correlations (also, negative correlations, with coefficients between 0 and -1). Therefore, calculate the absolute values of the weight coefficients, as values close to 0 imply weak statistical correlation of the peaks. 
** For each vertex, we aim to keep the associated edges with the largest absolute weight value: thus, iterating over the set of vertices, we can store all edges satisfying the requirement in a temporary "white-list" structure (e.g., a HashMap/Dictionary containing vertex start and end information for each selected edge). We can then iterate over the set of all edges in the graph, and delete those that are not included in the "white-list" structure described above. Note that in the resulting subgraph of the initial graph, some vertices may have more than 3 neighbors. 
5. The resulting two sub-graphs of the two initial graphs must be written to an xml file (GEXF format, readable by the Gephi software) i.e. two text files must be generated for each of the sub-graphs corresponding to the GEXF XSD schema (specification: https://gephi.org/gexf/format/schema.html , below is an example). 
6. The above program should be extended with the functionality to compute maximum spanning trees (MSTs) extracted from the subgraphs described above.

Notes: 
* Note that finding a maximum spanning tree is analogous to finding a minimum spanning tree, except that the weights must be inverted (or the comparator inverted, if using one). Kruskal's algorithm would be a good choice in this case. 
** To determine whether the add-edge operation forms a loop, recall that the Deep First Search / Breadth First Search algorithms can be useful. 
7. Similar to the task for item 5, save the resulting two covering trees in an xml file (GEXF format) i.e. generate a text file for each tree according to the GEXF XSD xml schema. 
8. Finally, open the resulting 2+2 xml files using Gephi software and try to manually enhance the visualization of the resulting graphs. From the Gephi menu, choose to export the graphs in PNG format.

### Results

* Краен резултат на получените графи от Gephi относно точка 4 и 5 от зададените изисквания:
* Final result of the graphs obtained by Gephi on point 4 and 5 of the given requirements:
  * Gephi файлът е с името graph_mtx_correl_ewm_vol.
  * Gephi file named raph_mtx_correl_ewm_vol.
  
  ![Exported Graph Image 2](https://github.com/gesho99/MDGP/blob/main/graph_mtx_correl_ewm_vol.png)

  * Gephi файлът е с името graph_mtx_correl_log_ret.
  * Gephi file named graph_mtx_correl_log_ret
  
  ![Exported Graph Image 3](https://github.com/gesho99/MDGP/blob/main/graph_mtx_correl_log_ret.png)

* Краен резултат на получените графи от Gephi относно точка 6 и 7 от зададените изисквания:
* Final result of the graphs obtained by Gephi on point 6 and 7 of the given requirements:
  * Gephi файлът е с името maximum_spanning_tree_mtx_correl_ewm_vol.
  * Gephi file named maximum_spanning_tree_mtx_correl_ewm_vol
  
  ![Exported Tree Image 1](https://github.com/gesho99/MDGP/blob/main/maximum_spanning_tree_mtx_correl_ewm_vol.png)

  * Gephi файлът е с името maximum_spanning_tree_mtx_correl_log_ret.
  * Gephi file named maximum_spanning_tree_mtx_correl_log_ret
  
  ![Exported Tree Image 1](https://github.com/gesho99/MDGP/blob/main/maximum_spanning_tree_mtx_correl_log_ret.png)
