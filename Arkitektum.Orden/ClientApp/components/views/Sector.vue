<template>
    <div>
        <div v-for="lawReference in apiData.lawReferences">
            <resource-link v-bind:resource-link="lawReference"></resource-link>
        </div>
        <input v-on:change="setApiData($event.target.value)" />
    </div>
</template>

<script src="./Sector.js"></script>
<style src="./Sector.scss"></style>
