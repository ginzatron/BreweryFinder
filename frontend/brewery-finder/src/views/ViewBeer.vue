<template>
    <section>
        
            <img src="#" alt="Beer Image">
            <p>Name</p>
        
        <p>ABV</p>
        <p>Style</p>
        <p>Favorite</p>
        <the-map></the-map>

        <table>
            <thead>
                <tr><th>Name</th>
                <th>Address</th>
                <th>Happy Hours</th>
                <th>Bar or Brewery</th>
                </tr>

            </thead>

            <tbody>
                <tr v-for="brewery in breweries" v-bind:key="brewery.id"><th>Name</th>
                <th>Address</th>
                <th>Happy Hours</th>
                <th>Bar or Brewery</th>
                </tr>

            </tbody>
        </table>
    </section>
</template>

<script>
import TheMap from '@/components/TheMap.vue'
export default {
    components:{
        TheMap,
    },
    data() {
        return {
            breweries:[]
        }
    },
    computed: {

    },
    methods: {

    },
    created(){
        const id=this.$route.params.id;
        fetch(`${process.env.vue_app_remote_api}/beer/${id}`, {
            method:"get"

        }).then(response=>response.json())
        .then(json=>{
            this.breweries=json;
        })
    }
}
</script>

<style>

</style>
