import { mapGetters, mapMutations, mapActions } from 'vuex';

/* Components */
import ResourceLink from '../modules/ResourceLink.vue';

const apiDataUrl = 'http://localhost:58288/Sectors/EditJson';

export default {
    name: 'Sector',
    components: {
        ResourceLink
    },
    computed: {
        ...mapGetters(['apiData'])
    },
    methods: {
        ...mapMutations([
            'setApiData'
        ]),
        testmethod() {
            console.log("testing");
        }
    },
    created() {
        this.$store.dispatch('getApiData', { url: apiDataUrl });
    }
}

