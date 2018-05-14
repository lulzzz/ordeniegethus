
import ListElement from './List/ListElement.vue'

export default {
    name: 'List',
    props: {
        name: String,
        writeAccess: {
            type: Boolean,
            default: false
        },
        apiUrl: String,
        listElements: Array,
        fields: Array,
        datasetId: String,
        listTitle: String
    },
    data() {
        return {
            newField: false,
            listToUpdate: null,
        }
    },
    components: {
        ListElement
    },
    computed: {
        listElementType() {
            if (this.listElements.length) {
                return typeof this.listElements[0];
            }
        }
    },
    mounted() {
        this.listToUpdate = this.listElements;
    },
    methods: {
        createNewField: function () {
            this.newField = true;

        },
        updateField(data, listElementIndex) {
            let editedList = {};
            editedList[this.name] = this.listToUpdate;
            editedList[this.name][listElementIndex] = data;
            editedList[this.name][listElementIndex];
            Promise.resolve(this.$root.putApiData(`/dataset/metadata/${this.datasetId}`, editedList));
        },
        postField(data) {
            let supplementedList = {};
            supplementedList[this.name] = this.listToUpdate;
            supplementedList[this.name].push(data);
            Promise.resolve(this.$root.putApiData(`/dataset/metadata/${this.datasetId}`, supplementedList))
                .then(() => {
                    this.getResourceLinks();
                });
            this.newField = false;
        },
        removeNewField() {
            this.newField = false;
        },
        removeField(listElementIndex) {
            let updatedList = {};
            updatedList[this.name] = this.listToUpdate;
            updatedList[this.name].splice(listElementIndex, 1);
            console.log(updatedList);
            Promise.resolve(this.$root.putApiData(`/dataset/metadata/${this.datasetId}`, updatedList))
                .then(() => {
                    this.getResourceLinks();
                });
        },
        createNewField() {
            this.newField = true;
        }

    }
}


