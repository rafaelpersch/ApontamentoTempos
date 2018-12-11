<template>
    <div>
        vou fazer...
    </div>
</template>

<script>
    import SessionService from '../services/SessionService';

    export default {
        data() {
            return {
            }
        },
        methods: {
        },
        created() {
            this.sessionService = new SessionService();

            if (this.sessionService.get() === null ){
                this.$router.replace({ name: "Home" });         
            }
        },
    }
</script>

<style scoped>
</style>