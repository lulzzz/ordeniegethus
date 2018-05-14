/* Components */
import List from '../modules/List.vue';
import DatasetMetadataConcepts from '../partials/DatasetMetadataConcepts.vue';

export default {
    name: 'DatasetMetadata',
    props: {
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        },
        lists: Array
    },
    components: {
        List,
        DatasetMetadataConcepts
    },
    data() {
        return {
            apiData: null,
            apiUrls: {
                getMetadata: `/dataset/metadata/${this.datasetId}`,
                getConcepts: `/api/concepts`
            }
        }
    },
    mounted() {
        this.getApiData();
    },
    methods: {
        getApiData() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.getMetadata))
                .then((apiData) => {
                    this.apiData = apiData;
                });
            Promise.resolve(this.$root.getApiData(this.apiUrls.getConcepts))
                .then((apiData) => {
                    this.apiData.concepts = apiData;
                }
                );


        }
    }
}

