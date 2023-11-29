var contest = {
    init: function () {
        contest.registerEvents();
    },
    registerEvents: function () {
        // Detail Contest
        $(document).on('click', '.detail', function () {
            var id = $(this).data('id');
            $.get('/Admin/Contest/Details', { id: id })
                .done(function (response) {
                    if (response.success) {
                        $('#modal-form').modal('show');
                        $('.modal-title').text('Hiển thị thông tin chi tiết cuộc thi');
                        // Update fields with contest details
                        $('#ContestNameDetail').text(response.data.contestName);
                        $('#DescriptionDetail').text(response.data.description);
                        $('#StartTimeDetail').text(response.data.startTime);
                        $('#EndTimeDetail').text(response.data.endTime);
                        $('#OwnerUserIdDetail').text(response.data.ownerUserId);
                    } else {
                        alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });
        // Save news contest
        $(document).on('click', '#btn-save', function () {
            var form = $('#form-contest');
            $.post($(this).attr('data-action'), form.serialize())
                .done(function (response) {
                    if (response.success) {
                        location.reload();
                    } else {
                        alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });
        // Save contest
        /*$(document).on('click', '#btn-save', function () {
            var form = $('#form-contest');
            var contestDetail = {
                ContestName: $('#ContestName').val(),
                Description: $('#Description').val(),
                StartTime: $('#StartTime').val(),
                EndTime: $('#EndTime').val(),
                OwnerUserId: $('#OwnerUserId').val()
            };
            var data = form.serializeArray();
            data.push({ name: 'contestDetail.ContestName', value: contestDetail.ContestName });
            data.push({ name: 'contestDetail.Description', value: contestDetail.Description });
            data.push({ name: 'contestDetail.StartTime', value: contestDetail.StartTime });
            data.push({ name: 'contestDetail.EndTime', value: contestDetail.EndTime });
            data.push({ name: 'contestDetail.OwnerUserId', value: contestDetail.OwnerUserId });

            $.ajax({
                url: $(this).data('action'),
                type: 'POST',
                data: $.param(data),
                success: function (response) {
                    if (response.success) {
                        location.reload();
                    } else {
                        alert(response.message);
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });*/

        // Edit contest
        $(document).on('click', '.edit', function () {
            var id = $(this).data('id');
            $.get('/Admin/Contest/Edit', { id: id })
                .done(function (response) {
                    if (response.success) {
                        $('#modal-form').modal('show');
                        $('.modal-title').text('Chỉnh sửa cuộc thi');
                        $('#btn-save').attr('data-action', '/Admin/Contest/Edit');
                        // Update fields with contest details
                        $('#ContestId').val(response.data.contestId);
                        $('#ContestName').val(response.data.contestName);
                        $('#Description').val(response.data.description);
                        $('#StartTime').val(response.data.startTime);
                        $('#EndTime').val(response.data.endTime);
                        $('#OwnerUserId').val(response.data.ownerUserId);
                    } else {
                        alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });

        // Delete contest
        $(document).on('click', '.delete', function () {
            var id = $(this).data('id');
            $('#delete-id').val(id);
            $('#modal-delete').modal('show');
        });

        // Delete contest
        $(document).on('click', '#btn-delete', function () {
            var id = $('#delete-id').val();
            $.post('/Admin/Contest/Delete', { contestId: id })
                .done(function (response) {
                    if (response.success) {
                        $('#modal-delete').modal('hide');
                        location.reload();
                    } else {
                        alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });

        // Create Contest
        $(document).on('click', '.create', function () {
            $('#modal-form').modal('show');
            $('.modal-title').text('Tạo mới cuộc thi');
            $('#btn-save').attr('data-action', '/Admin/Contest/Create');
            $('#form-contests').trigger('reset');
        });
    }
};
contest.init();
