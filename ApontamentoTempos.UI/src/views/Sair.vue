<template>
    <div>
    </div>
</template>

<script>
    import SessionService from '../services/SessionService';

    export default {
        created() {
            this.sessionService = new SessionService();
            this.sessionService.set(null);
            this.$router.replace({ name: "Home" });
        },
    }
</script>

<style scoped>
</style>