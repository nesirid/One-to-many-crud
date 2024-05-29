function loadCategoryArchivePage(url, containerId, page) {
    $.ajax({
        type: "GET",
        url: `${url}?page=${page}`,
        success: function (response) {
            $(`#${containerId} tbody`).empty();
            $(`#pagination-container`).empty();

            response.datas.forEach(item => {
                $(`#${containerId} tbody`).append(`
                    <tr class="category-data">
                        <td class="py-1">${item.categoryName}</td>
                        <td>${item.createdDate}</td>
                        <td>
                            <form class="d-inline" method="post" asp-area="Admin" asp-controller="Category" asp-action="Delete" asp-route-id="${item.id}">
                                <button type="submit" class="btn btn-danger text-white btn-sm"><i class="mdi mdi-delete"></i></button>
                            </form>
                            <button class="btn btn-secondary text-white btn-sm add-archive" data-id="${item.id}">Restore</button>
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
            console.error("Error occurred while loading categories:", error);
            console.error(xhr.responseText);
        }
    });
}
