/* Components */
import SidebarNavigation from '../modules/SidebarNavigation.vue';
import ResourceLinks from '../partials/ResourceLinks.vue';
import DatasetApplications from '../partials/DatasetApplications.vue';
import DatasetFields from '../partials/DatasetFields.vue';
import DatasetMetadata from '../partials/DatasetMetadata.vue';
import {
    format
} from 'date-fns';


export default {
    name: 'Dataset',
    props: {
        datasetId: String,
        writeAccess: {
            type: Boolean,
            default: false
        },
        metadataLists: Array
    },
    data() {
        return {
            apiUrls: {
                get: `/api/datasets/${this.datasetId}`,
                edit: `/datasets/edit/${this.datasetId}`
            },
            apiData: null,
            navigationItems: [{
                    name: 'Beskrivelse',
                    id: 'description'
                },
                {
                    name: 'Formål',
                    id: 'purpose'
                },
                {
                    name: 'Drift',
                    id: 'hosting'
                },
                {
                    name: 'Informasjonselementer',
                    id: 'fields'
                },
                {
                    name: 'Metadata',
                    id: 'metadata'
                },
                {
                    name: 'Lenker',
                    id: 'resource-links'
                },
                {
                    name: 'Applikasjoner',
                    id: 'applications'
                }
            ]
        }
    },
    components: {
        SidebarNavigation,
        ResourceLinks,
        DatasetApplications,
        DatasetFields,
        DatasetMetadata
    },
    mounted() {
        this.getApiData();
    },
    methods: {
        getApiData() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.get))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },

    },
    filters: {
        formatDate: function (value) {
            if (!value) return '';
            return format(value, 'D. MMMM YYYY HH:mm:ss');
        },
        formatBoolean: function (value) {
            if (value)
                return 'Ja';
            else
                return 'Nei';
        }
    }
}