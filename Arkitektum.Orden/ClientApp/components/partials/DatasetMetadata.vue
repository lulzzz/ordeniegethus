<template>
  <div>
      <!--<list v-bind:list-elements="apiData.concepts" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="Temaer"  v-bind:dataset-id="datasetId"></list>-->
      <!--<list v-bind:list-elements="apiData.accessRightComments" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="AccessRightComments"  v-bind:dataset-id="datasetId">
      </list>
      <list v-bind:list-elements="apiData.contactPoints" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="ContactPoints"  v-bind:dataset-id="datasetId"></list>
      <list v-bind:list-elements="apiData.distributions" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="Distributions"  v-bind:dataset-id="datasetId"></list>
      <list v-bind:list-elements="apiData.identifiers" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="Identifiers"  v-bind:dataset-id="datasetId"></list>
      <list v-bind:list-elements="apiData.subjects" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="Subjects"  v-bind:dataset-id="datasetId"></list>
      <list v-bind:list-elements="apiData.keywords" v-bind:apiUrl="url" v-bind:write-access="writeAccess" name="Keywords"  v-bind:dataset-id="datasetId"></list>-->

    <list v-bind:list-elements="apiData[list.propertyName]" v-bind:listTitle="list.listTitle" v-bind:apiUrl="url" v-bind:write-access="writeAccess" v-bind:name="list.name"  v-bind:dataset-id="datasetId" v-for="list in lists"></list>
  </div>
</template>

<script src="./DatasetMetadata.js"></script>
<style src="./DatasetMetadata.scss"></style>