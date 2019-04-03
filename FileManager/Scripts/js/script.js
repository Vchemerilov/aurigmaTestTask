

    var filesFolderdata = [];

    //Вывести картинку
    function showPicture(id) {
        $("#text").html("");
        $("#picture").attr("src", "/Home/ReadPicture/" + id);
        $("#picture").show();
    }

    //Вывести текст
    function showText(id) {
        $.ajax({
            type: "GET",
            url: "/Home/ReadText/" + id,
            success: function (data) {
                $("#picture").hide();
                $("#text").html(data);
            },
            error: function (data) {
                alert("Error: " + data.id);
            }
        })
    }

    //Получить список данных о файлах в папке
    function getFileList() {
        $.ajax({
            type: "GET",
            url: "/Home/getFileList/",
            success: function (data) {
                filesFolderdata = JSON.parse(data);
                createRowsTable();
            },
            error: function () {
                alert("Error");
            }
        })

    }

    //Получить имя файла по id
    function getFileNameFromId(id) {
        var fileFolder = filesFolderdata.filter(file =>file.id == id)[0];
        return fileFolder.name;
    }

    // Оформить данные в виде таблицы
    function createRowsTable() {
        var stringfile = "";

        filesFolderdata.forEach(function (filedata) {
            stringfile += getStringfile(filedata);
        });

        $("#tablebody").html(stringfile);
        workData();
    }

    // Получить данные таблицы в виде строки
    function getStringfile(dataFile) {
        var stringdata = "";
        stringdata += "<tr>" +
               "<td class=\"col-md-4\"><p class=\"text-center\">" + dataFile["name"] + " </p></td> " +
               "<td class=\"col-md-3\"><p class=\"text-center\">" + dataFile["size"] + "</p></td>" +
        "<td class=\"col-md-4\"><p class=\"text-center\">" + dataFile["lastModification"] + "</p></td>" +
               "<td class=\"col-md-3\">";

        if (dataFile["extension"] == ".txt" || dataFile["extension"] == ".jpg" ||
            dataFile["extension"] == ".png" || dataFile["extension"] == "jpeg" || dataFile["extension"] == ".gif") {
            stringdata += "<a href=\"#\" class=\"readFile\" id=\"" + dataFile["id"] + "\">Проcмотреть</a>";
        }
        stringdata += "</td>" +

               "<td class=\"col-md-3\"><a href=\"#\" class=\"removeLink\" id=\"" + dataFile["id"] + "\">Удалить</a></td>" +
           "</tr>";

        return stringdata;
    }


    function workData() {

        // Удалить файл из папки
        $(".removeLink").click(function () {
            let data = $(this).attr("id");
            $.ajax({
                type: "POST",
                url: "/Home/RemoveFile/",
                data: { id: data },
                success: function (data) {
                    getFileList();
                },
                error: function () {
                    alert("Error");
                }
            })

        })
        
        // Прочитать данные файла в модальном окне (картинка, текст)
        $(".readFile").click(function () {
            var fileid = $(this).attr("id");
            var filename = getFileNameFromId(fileid);

            if (filename.toLowerCase().endsWith(".txt"))
                showText(fileid);
            else if (filename.toLowerCase().endsWith(".jpg") || filename.toLowerCase().endsWith(".png") ||
                     filename.toLowerCase().endsWith(".jpeg") || filename.toLowerCase().endsWith(".gif"))
                showPicture(fileid);

            $(".modal-title").text(filename);
            $("#myModal").modal('show');

        })

    }

    $(document).ready(function () {

        getFileList();
        workData();

        // Сохранить файл в папку на сервере
        $("#form1").submit(function () {

            var formData = new FormData($(this)[0]);

            $.ajax({
                url: $(this).attr('action'),
                type: $(this).attr('method'),
                data: formData,
                async: true,
                success: function (data) {
                    alert(data);
                    getFileList();

                },
                error: function () {
                    alert('Ошибка');
                },
                cache: false,
                contentType: false,
                processData: false
            });
            return false;
        });
    });