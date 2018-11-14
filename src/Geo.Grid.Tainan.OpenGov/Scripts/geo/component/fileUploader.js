(function (geo) {
    "use strict";

    if (typeof geo === undefined) {
        geo = {};
    }

    var component = {
        props: {
            extension: {
                type: String,
                default:""
            },
            comid: {
                type: String,
                default: ""
            }
        },
        data: function () {
            return {
                file: null,
                note: ""
            };
        },
        computed: {
            fileName: function () {
                return this.file ? this.file.name : "請選擇要上傳的檔案";
            },
            extensionName:function() {
                return this.extension === "" ? "PDF檔、WORD檔" : this.extension + "檔";
            },
            realId:function() {
                return this.comid === "" ? "fileUpload-pop" : this.comid;
            }
        },
        template:
                '<div class="modal " v-bind:id="realId" tabindex="-1"  role="dialog" aria-hidden="true">\
                <div class="modal-dialog">\
                    <div class="modal-content">\
                        <div class="modal-header">\
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>\
                            <h4 class="modal-title" id="myModalLabel">檔案上傳</h4>\
                        </div>\
                        <div class="modal-body">\
                            <table class="table upload-pop-table">\
                                <tr>\
                                    <td colspan="2">檔案屬性</td>\
                                </tr>\
                                <tr style="display:none">\
                                    <th>檔案說明</th>\
                                    <td>\
                                        <input type="text"  v-bind:value="note" v-on:change="changeNote" id="txtAlt" class="form-control" placeholder="輸入檔案說明" />\
                                    </td>\
                                </tr>\
                                <tr>\
                                    <th>上傳檔案</th>\
                                    <td>\
                                        <button class="btn bg-gray"  v-on:click="fileClick()"  id="btnFile">選擇檔案</button>\
                                        <span id="spanFileName">{{fileName}}</span>\
                                        <input type="file" id="file" v-on:change="fileChange()" style="display:none"/>\
                                    </td>\
                                </tr>\
                                <tr>\
                                    <th>注意事項</th>\
                                    <td>上傳檔案格式僅限 {{extensionName}}。</td>\
                                </tr>\
                            </table>\
                        </div>\
                        <div class="modal-footer">\
                            <button type="button" class="btn btn-default" data-dismiss="modal">關閉</button>\
                            <button type="button" class="btn bg-green" v-on:click="upload()">存檔</button>\
                        </div>\
                    </div>\
                </div>\
            </div>',

        methods: {
            fileClick: function () {
                $(this.$el).find('#file').click();
            },

            fileChange: function () {
                this.file = $(this.$el).find('#file').get(0).files[0];
            },
            changeNote: function(event) {
                this.note = event.target.value;
            },
            upload: function () {
                this.$emit("submit", this.file, this.note);
                this.file = null;
                this.note = '';
            }
        },
        mounted: function () {
            this.$data.fileName = '請選擇要上傳的檔案';
        }
        
    };

    geo.fileUploader = component;

})(geo);