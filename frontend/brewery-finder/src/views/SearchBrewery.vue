<template>

</template>

<script>
export default {
    data() {
        return {
            breweries : []
        }
    },

    methods: {
        loadBreweries() {
            fetch (`${process.env.VUE_APP_REMOTE_API}/brewery?zip=44145`,{
                method:"GET",

            }).then(response => response.json())
            .then(json => this.breweries = json)
        }
    }
}
</script>

<style>

</style>
