export default {
    name: 'SidebarNavigation',
    props: {
        navigationItems: Array,
    }, 
    data() {
        return {
            path: window.location.pathname
        }
    },
    components: {

    },
    mounted() {
    },
    methods: {
        getUrl(navigationItem){
            return `${this.path}#${navigationItem.id}`;
        }
    },
    filters: {
    }
}

