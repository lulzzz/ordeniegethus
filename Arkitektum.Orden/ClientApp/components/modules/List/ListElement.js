export default {
    name: 'ListElement',
    props: {
        name: String,
        writeAccess: {
            type: Boolean,
            default: false
        },
        apiUrl: String,
        listElement: null,
        listElementType: String,
        saved: Boolean,
        fields: Array,
        listElementIndex: Number
    },
    data() {
        return {
            editable: false,
            data: this.listElement !== undefined ? this.listElement : null
        }
    },
    mounted() {
        if (this.listElement === undefined)
        {
            if (this.listElementType == 'object') {
                this.data = {};
                this.fields.forEach(field => {
                    this.data[field] = '';
                });
            } else {
                this.data = '';
            }
        }
    },
    methods: {
        update() {
            this.$parent.updateField(this.data, this.listElementIndex);
            this.editable = false;
        }
    }

}

