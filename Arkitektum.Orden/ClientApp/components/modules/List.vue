<template>
  <div class="list-group-item">
    <label class="col-sm-2 col-form-label" >{{listTitle}}</label>

    <!--Saved field-->
    <div v-for="(listElement, listElementIndex) in listElements" >
      <list-element v-bind:list-element="listElement" v-bind:list-element-type="listElementType"
                    v-bind:list-element-index="listElementIndex"
                    v-bind:fields="fields" v-bind:saved="true"
                    v-bind:write-access="writeAccess" v-on:remove="removeField(listElementIndex)"  :key="listElement.name">
      </list-element>
    </div>

    <!--New field-->

    <list-element v-bind:saved="false" v-bind:list-element-type="listElementType" v-bind:fields="fields" v-bind:write-access="writeAccess" v-if="newField"></list-element>

    <div  v-if="writeAccess">
      <button v-show="!newField"  v-on:click="createNewField()" class="btn btn-outline-primary">
        <span class="fas fa-plus"></span> Ny {{ name }}
      </button>
    </div>
  </div>
</template>

<script src="./List.js"></script>
<style lang="scss" src="./List.scss"></style>

