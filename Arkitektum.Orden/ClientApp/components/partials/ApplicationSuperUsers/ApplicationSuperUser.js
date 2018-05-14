/* Components */
import FilterSelect from '../../modules/FilterSelect.vue';

export default {
    name: 'ApplicationSuperUser',
    props: {
        applicationSuperUser: Object,
        availableSuperUsers: Array,
        saved: Boolean,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        FilterSelect
    },
    data() {
        return {
            editable: false,
            data: this.applicationSuperUser !== undefined ? this.applicationSuperUser : { name: '' }
        }
    },
    methods: {
        selectApplicationSuperUser(applicationSuperUser) {
            this.data = applicationSuperUser;
        },
        datasetDetailsPageUrl() {
            return `/datasets/details/${this.applicationSuperUser.id}`;
        }
    }
}
