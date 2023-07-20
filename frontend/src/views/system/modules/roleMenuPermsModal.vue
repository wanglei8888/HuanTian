<template>
  <a-modal :title="title" :width="1000" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="formLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="所属应用" prop="menuType">
              <a-select style="width: 100%" v-model="form.menuType" placeholder="请选择应用分类">
                <a-select-option v-for="(item, index) in appData" :key="index" :value="item.value"
                  @click="changeApplication(item.value)">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <div>
              <a-form-model-item label="所属菜单" prop="menuId">
                <a-tree-select style="width: 100%" v-model="form.menuId"
                  :dropdownStyle="{ maxHeight: '300px', overflow: 'auto' }" :treeData="menuTreeData" :replaceFields="{
                    key: 'id',
                    title: 'name',
                    value: 'id'
                  }" @change="menuChange" treeDefaultExpandAll>
                  <span slot="title" slot-scope="{ title }">{{ title }}
                  </span>
                </a-tree-select>
              </a-form-model-item>
            </div>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="菜单按钮" prop="permissionsId" hasFeedback>
              <a-select mode="multiple" v-model="form.permissionsId" placeholder="请选择菜单按钮" :allowClear="true"
                :filterOption="filterOption">
                <a-select-option v-for="(value, key) in menuPerms" :key="key" :value="value.id">
                  {{ value.name }}
                </a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>

import IconSelector from '@/components/IconSelector'
import { sysDict } from '@/api/system'

export default {
  components: { IconSelector },
  data() {
    return {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 5 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 17 }
      },
      gutter: 24,
      mdSize: 12,
      smSize: 24,
      formLoading: false,
      title: '菜单按钮',
      visible: false,
      confirmLoading: false,
      appData: [],
      menuPerms: [],
      menuTreeData: [],
      rules: {
        menuType: [{ required: true, message: '请选择应用分类' }],
        menuId: [{ required: true, message: '请选择所属菜单！' }],
        permissionsId: [{ required: true, message: '请选择所属菜单！' }],
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    create(roleInfo) {
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      // 获取下拉数据
      this.getDropdown()
      this.form = createForm()
      this.form.roleId = roleInfo
      this.visible = true
      // this.formLoading = true
      this.changeApplication(this.form.menuType)
      // this.handSerachMenuPerms()
    },
    filterOption(input, option) {
      return (
        option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
      );
    },
    menuChange(e) {
      this.handSerachMenuPerms()
    },
    // 获取下拉数据
    getDropdown() {
      sysDict({ code: 'MenuType' }).then((res) => {
        if (res.code === 200) {
          this.appData = res.result
        }
      })
    },
    // 查询已经选中的菜单按钮信息
    handSerachRolePerms() {
      this.$http.get('/sysPermissions/rolePermission', { params: { roleId: this.form.roleId, menuId: this.form.menuId } }).then(res => {
        if (res.code === 200) {
          this.form.permissionsId = res.result.map(t => t.id)
        } else {
          this.$message.warning(res.message)
        }
      }).finally(() => {
        this.formLoading = false
      })
    },
    // 获取菜单按钮
    handSerachMenuPerms() {
      this.form.permissionsId = []
      this.$http.get('/sysPermissions', { params: { menuId: this.form.menuId } }).then(res => {
        if (res.code === 200) {
          this.menuPerms = res.result
          this.handSerachRolePerms()
        } else {
          this.$message.warning(res.message)
        }
      })
    },
    changeApplication(value) {
      this.$http.get('/sysMenu/tree', { params: { menuType: value } }).then(res => {
        if (res.code === 200) {
          this.menuTreeData = res.result
        } else {
          this.$message.warning(res.message)
        }
      })
    },
    close() {
      this.$emit('close')
      this.visible = false
    },
    handleOk() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.confirmLoading = true
          let submitData = { roleId: this.form.roleId, permissionsId: this.form.permissionsId }
          this.$http.post('/sysRole/AddRolePerms', submitData).then(res => {
            if (res.code === 200) {
              this.$emit('ok')
              this.confirmLoading = false
              this.visible = false
            } else {
              this.$message.warning(res.message)
            }
          }).finally(() => {
            this.confirmLoading = false
          })
        }
      })
    },
    handleCancel() {
      this.close()
    }
  }

}
function createForm() {
  return {
    menuType: 'Business',
    icon: 'none',
    show: true,
    // menuId: '417244902117909',
    permissionsId: []
  }
}
</script>
