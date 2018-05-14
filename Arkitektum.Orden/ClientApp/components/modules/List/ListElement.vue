<template>
  <div>
    <!--Saved field-->
    <div v-if="saved">

      <div v-show="editable">
        <div v-if="listElementType == 'object'">
          <div  v-for="field in fields">
            <div class="form-group row">
              <input v-bind:name="field.name" v-bind:value="listElement[field.name]" />
            </div>
          </div>
        </div>

        <div v-else  class="d-inline-block">
          <input v-bind:name="name"  v-model="data" />
          <div class="float-right">
            <span class="btn btn-outline-secondary" v-on:click="editable = !editable">
              <span class="fas fa-times"></span> Avbryt
            </span>
            <span class="btn btn-outline-success" v-on:click="update()">
              <span class="fas fa-check"></span> Oppdater
            </span>
          </div>
        </div>
      </div>

      <div v-show="!editable" v-bind:class="{'d-flex justify-content-between align-items-center': !editable}">

        <div v-if="listElementType == 'object'" class="d-inline-block">
          <div v-for="field in fields">
            <p>{{ listElement[field.name] }}</p>
          </div>
        </div>

        <div v-else="" class="d-inline-block">
          <p>{{ listElement }}</p>
        </div>
        <div class="float-right" v-if="writeAccess">
          <span class="btn btn-outline-secondary" v-on:click="editable = !editable">
            <span class="fas fa-edit"></span>
          </span>
          <span class="btn btn-outline-danger" v-on:click="$emit('remove')">
            <span class="fas fa-trash"></span>
          </span>
        </div>
      </div>
    </div>


    <!--New field-->
    <div v-else="" class="list-group-item">

      <label for="listElementType.name" class="col-sm-2 col-form-label">Ny felt</label>

      <div v-if="listElementType == 'object'">
        <div class="form-group row" v-for="field in fields">
          <input v-bind:name="field.name" v-bind="listElement[field.name]" class="form-control"  />
        </div>
      </div>

      <div v-else="">
        <input v-bind:name="name" v-model="data"  />
        <div class="float-right">
          <span class="btn btn-outline-danger" v-on:click="$parent.removeNewField()">
            <span class="fas fa-trash"></span> Avbryt
          </span>
          <span class="btn btn-outline-success" v-on:click="$parent.postField(data)"  v-bind:editable="false">
            <span class="fas fa-check"></span> Legg til
          </span>
        </div>

      </div>
    </div>



  </div>
</template>

<script src="./ListElement.js"></script>
<style lang="scss" src="./ListElement.scss"></style>


