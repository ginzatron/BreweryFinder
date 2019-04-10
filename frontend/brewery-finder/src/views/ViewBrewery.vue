<template>
    <section>
        
        <p>{{name}}</p>
        <p>Happy Hour(s): {{happyHourFrom}} to {{happyHourTo}}</p>
        <p>Est. {{established}}</p>
        <p>{{address}}</p>
        <p>{{city}} {{state}}</p>
        <p>{{zip}}</p>
        

        <p>{{description}}</p>
        
    <p>Beers Available</p>
    
    <beer-info v-for="beer in beers" v-bind:key="beer.id"></beer-info>



    </section>
</template>

<script>
export default {

}
</script>

<style>

</style>
