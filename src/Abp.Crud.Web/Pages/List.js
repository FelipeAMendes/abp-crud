$(function () {
    abp.log.debug('List.js initialized!');

    $('#personsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            ajax: abp.libs.datatables.createAjax(abp.crud.services.persons.getList),
            columnDefs: [
                {
                    title: 'FirstName',
                    data: "firstName"
                },
                {
                    title: 'LastName',
                    data: "lastName"
                },
                {
                    title: 'Email',
                    data: "email"
                },
                {
                    title: 'BirthDate',
                    data: "birthDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                }
            ]
        })
    );
});
