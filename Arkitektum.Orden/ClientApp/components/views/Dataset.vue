<template>
    <div>
        
        <dataset-applications></dataset-applications>
        <dataset-fields v-bind:dataset-id="datasetId" write-access></dataset-fields>
    </div>
</template>


<script src="./Dataset.js"></script>
<style src="./Dataset.scss"></style>
