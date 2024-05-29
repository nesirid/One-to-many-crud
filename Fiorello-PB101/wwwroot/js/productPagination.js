function loadProductPage(url, containerId, page) {
    $.ajax({
        type: "GET",
        url: `${url}?page=${page}`,
        success: function (response) {
            $(`#${containerId} tbody`).empty();
            $(`#pagination-container`).empty();

            response.datas.forEach(item => {
                $(`#${containerId} tbody`).append(`
                    <tr class="category-data">
                        <td class="py-1">
                            <img src="/img/${item.mainImage}" alt="${item.name}" />
                        </td>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>${item.price}</td>
                        <td>${item.categoryName}</td>
                        <td>
                            <a class="btn btn-primary text-white btn-sm" href="#"><i class="mdi mdi-information-variant"></i></a>
                            <a class="btn btn-warning text-white btn-sm" href="/Admin/Product/Edit/${item.id}"><i class="mdi mdi-grease-pencil"></i></a>
                            <form class="d-inline" method="post" action="/Admin/Product/Delete/${item.id}">
                                <button type="submit" class="btn btn-danger text-white btn-sm"><i class="mdi mdi-delete"></i></button>
                            </form>
                        </td>
                    </tr>
                `);
            });

            $(`#pagination-container`).append(`
                <li class="page-item ${response.hasPrevious ? "" : "disabled"}">
                    <a class="page-link" href="javascript:void(0)" data-page="${response.currentPage - 1}">Previous</a>
                </li>
            `);
            for (let i = 1; i <= response.totalPage; i++) {
                $(`#pagination-container`).append(`
                    <li class="page-item ${response.currentPage == i ? "disabled" : ""}">
                        <a class="page-link ${response.currentPage == i ? "paginate-active" : ""}" href="javascript:void(0)" data-page="${i}">${i}</a>
                    </li>
                `);
            }
            $(`#pagination-container`).append(`
                <li class="page-item ${response.hasNext ? "" : "disabled"}">
                    <a class="page-link" href="javascript:void(0)" data-page="${response.currentPage + 1}">Next</a>
                </li>
            `);
        },
        error: function (xhr, status, error) {
            console.error("Error occurred while loading products:", error);
            console.error(xhr.responseText);
        }
    });
}
