<template>
    <div>
        <div v-if="apiData != null" v-for="lawReference in apiData.lawReferences">
            <resource-link v-bind:resource-link="lawReference"></resource-link>
        </div>
        <input />
    </div>
</template>

<script src="./Sector.js"></script>
<style src="./Sector.scss"></style>
